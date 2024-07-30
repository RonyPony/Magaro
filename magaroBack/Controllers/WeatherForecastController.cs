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
            
            IEnumerable<Message> data = serv.readAllMessages();
            return Ok(data);
        }

        [HttpGet("/getOnlineUsers")]
        public IActionResult GetOnline()
        {
           
            IEnumerable<string> data = serv.GetOnlineUsers();
            return Ok(data);
        }
    }
}
