using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceA.Data;
using ServiceA.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServiceA.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ApplicationDbContext context, ILogger<MessagesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMessage()
        {

            try{
            var random = new Random();
            if (random == null)
    {
        _logger.LogError("Random object is null.");
        throw new Exception("Random object is null.");
    }
            var message = new Message
            {
                
                Number = random.Next(1,1000)
            };

if (message == null)
    {
        _logger.LogError("Message object is null.");
        throw new Exception("Message object is null.");
    }

            _context.Messages.Add(message);
_logger.LogInformation($"New Message: {message.Id} {message.Number}");
            await _context.SaveChangesAsync();
  _logger.LogInformation($"Saved message with ID {message.Id} and number {message.Number} to the database.");

            return Ok(new { message.Id, message.Number });
        }

        catch (Exception ex){
            _logger.LogError(ex, "An error occurred while saving the entity changes.");
        return StatusCode(500, "An error occurred while saving the entity changes.");
    }
        }
        }

        
    }
