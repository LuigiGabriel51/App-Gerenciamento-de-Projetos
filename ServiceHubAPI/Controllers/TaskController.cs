using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceHub.DataBase;
using ServiceHubAPI.Model;
using System.Linq;

namespace ServiceHubAPI.Controllers
{
    [Route("/Projects")]
    public class TaskController: Controller
    {
        private readonly DBContextAPI context;
        public TaskController(DBContextAPI context) 
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult getProjects(string nome)
        {
            if (nome == string.Empty) { return BadRequest(); }
            var projects = context.Tasks.ToList();
            var stages = context.Stages.ToList();

            List<int> IdsProject = new List<int>();           
            foreach (var project in projects)
            {
                List<string> userList = project.Users.Split(',').Select(s => s.Trim()).ToList();
                if (userList.Contains(nome))
                {
                    IdsProject.Add(project.Id);
                }
            }

            List<Project> Projects = new();
            foreach (var IDs in IdsProject) 
            {
                List<StageModel> inE= new List<StageModel>();
                foreach (var stage in stages)
                {
                    if(stage.Id_Task == IDs) 
                    {
                        inE.Add(stage);
                    }
                }
                var inProject = new Project()
                {
                    Stages = inE,
                    Tasks = projects.Where(x => x.Id == IDs).FirstOrDefault()
                };

                string Usuarios = string.Empty;
                List<string> userList = inProject.Tasks.Users.Split(',').Select(s => s.Trim()).ToList();
                if (userList.Contains(nome))
                {
                    foreach (var user in userList)
                    {
                        Usuarios += user;
                    }
                }
                inProject.Tasks.Users = Usuarios;
                Projects.Add(inProject);
            }

            return Ok(Projects);
        }

        // routes Projects
        [HttpPost]
        public async Task<ActionResult> postProject([FromBody] TaskModel project)
        {
            if(project == null) { return BadRequest(); }
            if(project.Image == null) project.Image = new byte[0];
            await context.Tasks.AddAsync(project);
            await context.SaveChangesAsync();
            return Ok(StatusCode(200));
        }
        [HttpPut]
        public async Task<ActionResult> PutProect([FromBody] TaskModel project)
        {
            if (project == null) { return BadRequest(); }
            context.Tasks.Update(project);
            await context.SaveChangesAsync();
            return Ok(StatusCode(200));
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteProect([FromBody] TaskModel project)
        {
            if (project == null) { return BadRequest(); }
            context.Tasks.Remove(project);
            await context.SaveChangesAsync();
            return Ok(StatusCode(200));
        }

        // routes Stage
        [HttpPut("/PutStage")]
        public async Task<ActionResult> PutStage([FromBody] StageModel stage)
        {
            if (stage == null) { return BadRequest(); }
            context.Stages.Update(stage);
            await context.SaveChangesAsync();
            return Ok(StatusCode(200));
        }

        [HttpPost("/AddStage")]
        public async Task<ActionResult> postStage([FromBody] StageModel stage)
        {
            if (stage == null) { return BadRequest(); }
            await context.Stages.AddAsync(stage);
            await context.SaveChangesAsync();
            return Ok(StatusCode(200));
        }
        [HttpDelete("/DeleteStage")]
        public async Task<ActionResult> DeleteStage([FromBody] StageModel stage)
        {
            if (stage == null) { return BadRequest(); }
            context.Stages.Remove(stage);
            await context.SaveChangesAsync();
            return Ok(StatusCode(200));
        }
    }
    public class Project
    {
        public TaskModel Tasks { get; set; }
        public List<StageModel> Stages { get; set; }
    }
}
