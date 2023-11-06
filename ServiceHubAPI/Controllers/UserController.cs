using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceHub.DataBase;
using ServiceHub.Model;
using ServiceHubAPI.Model;

namespace ServiceHub.Controllers
{
    [Route("/User")]
    public class UserController : Controller
    {
        private readonly DBContextAPI _context;

        public UserController(DBContextAPI context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var Users = await _context.Users.ToListAsync();
            return Ok(Users);
        }

        [HttpPost]
        public async Task<ActionResult> PostUsers([FromBody] UserModel user)
        {
            if(user == null) { return BadRequest(); }
            await _context.Users.AddAsync(user);
            _context.SaveChanges();
            return Ok(StatusCode(200));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if(id == 0) { return BadRequest();}
            var user = await _context.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
            if(user == null) { return BadRequest(); }
            _context.Users.Remove(user);
            return Ok(StatusCode(200));
        }

        [HttpPut]
        public ActionResult PutUser([FromBody] UserModel User)
        {
            if (User == null) { return BadRequest(); }
            try
            {
                _context.Users.Update(User);
                _context.SaveChangesAsync();
                return Ok(User);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/Login")]
        public async Task<ActionResult> LoginUser(string CPF, string Password)
        {
            if(string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(CPF)) { return BadRequest(); }
            var User = await _context.Users.Where(user => user.CPF == CPF).FirstOrDefaultAsync();
            if(User == null) { return BadRequest(); }
            if (Password == User.Password)
            {
                return Ok(User);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/UpdateTK")]
        public ActionResult PutToken(int id, string token)
        {
            if (id == 0 || token == null) {  return BadRequest(); }
            var User = _context.Users.Where(user => user.Id == id).FirstOrDefault();
            if (User == null) { return BadRequest(); };
            User.Token = token;
            User.ExpireToken = DateTime.Now.AddDays(1);
            _context.Users.Update(User);
            _context.SaveChanges();
            return Ok(StatusCode(200));
        }

    }
}
