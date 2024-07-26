using magaroBack.Interface;
using magaroBack.Model;
using Microsoft.AspNetCore.Mvc;

namespace magaroBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatHub serv;

        private readonly ILogger<ChatController> _logger;

        public ChatController(ILogger<ChatController> logger, IChatHub hub)
        {
            _logger = logger;
            serv = hub;
        }

        [HttpGet("/getAllMessages")]
        public IActionResult Get()
        {
            List<Message> messages = new List<Message>();
            for (int i = 0; i < 50; i++)
            {
                Message message = new Message
                {
                    message = "Este mensaje lo envio el usuario No." + i.ToString(),
                    sendDate = DateTime.Now,
                    username = "Usuario No." + i.ToString()
                };
                messages.Add(message);
            }
            IEnumerable<Message> data = serv.readAllMessages();
            return Ok(data);
        }
    }
}
