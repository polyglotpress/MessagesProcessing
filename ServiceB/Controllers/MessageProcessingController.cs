using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceB.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceB.Data;
using ServiceB.Models;
using StackExchange.Redis;


namespace ServiceB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageProcessingController : ControllerBase
    {

        private readonly MessageProcessingService _messageProcessingService;
        // private readonly ApplicationDbContext _context;
        // private readonly IDatabase _redisDatabase;
        // private readonly ILogger<MessageProcessingController> _logger;
        // private readonly MessageProcessingService _messageProcessingService;

        public MessageProcessingController(MessageProcessingService messageProcessingService)
        {
            _messageProcessingService = messageProcessingService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessMessages()
        {
          await _messageProcessingService.ProcessMessagesAsync();

           return Ok();
        }
    }
}
