using CommunityToolkit.Mvvm.ComponentModel;
using ServiceHub.Model;

namespace ServiceHub.ViewModel
{
    public class VMHomePage : ObservableObject
    {
        private int numestagio;
        public int numEstagio
        {
            get => numestagio;
            set => SetProperty(ref numestagio, value);
        }

        private int numproject;
        public int NumProjects
        {
            get => numproject;
            set => SetProperty(ref numproject, value);
        }
        private RestService rest;
        private List<StageModel> st;
        public List<StageModel> stages
        {
            get => st;
            set => SetProperty(ref st, value);
        }
        public string Name { get; set; }
        private byte[] image { get; set; }
        private Permission permission { get; set; }
        public string Cargo
        {
            get
            {
                if (permission == Permission.NV2) return "Chefe";
                if (permission == Permission.NV3) return "Gerente";
                return "Funcionário";
            }
        }
        public ImageSource ImagePerfil
        {
            get
            {
                if (image != null && image.Length > 0)
                {
                    return ImageSource.FromStream(() => new MemoryStream(image));
                }
                return null;
            }
        }
        public VMHomePage()
        {
            rest = new RestService();
            image = InicializeApp.User.Image;
            Name = InicializeApp.User.Name;
            permission = InicializeApp.User.LevelPermission;
            getProject();
        }
        private async Task getProject()
        {
            var stages_iconplete = new List<StageModel>();
            var Projects = await rest.GetProjects(InicializeApp.User.Name);
            foreach (var project in Projects)
            {
                foreach (var stage in project.Stages.Where(x => x.DateInit.Day == DateTime.Now.Day))
                {
                    stages_iconplete.Add(stage);
                }
            }
            stages = stages_iconplete;
            numEstagio = stages.Count;
            NumProjects = Projects.Count;
        }
    }
}
