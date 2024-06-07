using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; // Add this namespace for logging
using ServiceB.Data;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceB.Services
{
    public class MessageProcessingService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly ILogger<MessageProcessingService> _logger; // Add logger

        public MessageProcessingService(ApplicationDbContext dbContext, IConnectionMultiplexer redisConnection, ILogger<MessageProcessingService> logger)
        {
            _dbContext = dbContext;
            _redisConnection = redisConnection;
            _logger = logger; // Inject logger
        }

        public async Task ProcessMessagesAsync()
        {
            try
            {
                _logger.LogInformation("Starting message processing...");

                var messages = await _dbContext.Messages.ToListAsync(); // Retrieve messages from the database
                _logger.LogInformation($"Retrieved {messages.Count} messages from the database.");

                var db = _redisConnection.GetDatabase();

                foreach (var message in messages)
                {
                    // Process the number (e.g., double it)
                    var processedNumber = message.Number * 2;

                    // Store the processed number in Redis
                    await db.StringSetAsync($"ProcessedNumber:{message.Id}", processedNumber);
                    _logger.LogInformation($"Stored processed number {processedNumber} in Redis for message with ID {message.Id}.");
                }

                _logger.LogInformation("Message processing completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during message processing.");
                throw; // Rethrow the exception to ensure it's propagated
            }
        }
    }
}
