using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceHub.DataBase;
using ServiceHubAPI.Model;

namespace ServiceHubAPI.Controllers
{
    [Route("/Message")]
    public class MessageController: Controller
    {
        private readonly DBContextAPI _context;
        public MessageController() 
        {
            _context = new DBContextAPI();
        }

        [HttpGet]

        public IActionResult Get() 
        {

            var AllChat = _context.Messages.ToList();
            return Ok(AllChat);
        }

        [HttpPost("/SendMessage")]
        public async Task<ActionResult> SendMessage([FromBody] MessageModel Message)
        {
            if (Message == null) { return BadRequest(); }
            var registrationTokens = new List<string>();
            var users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                registrationTokens.Add(user.Token);
            }

            foreach (var registrationToken in registrationTokens)
            {
                var message = new Message()
                {
                    Notification = new Notification
                    {
                        Title = "ServiceHub Notificação",
                        Body = $"{Message.User}: {Message.Message}"
                    },
                    Token = registrationToken
                };
                var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }

            if (Message.Image == null) { Message.Image = new byte[256]; }
            await _context.Messages.AddAsync(Message);
            await _context.SaveChangesAsync();
            return Ok(StatusCode(200));
        }    

    }
}
