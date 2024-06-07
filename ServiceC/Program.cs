using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace ServiceC
{
    class Program
    {
        private static readonly string redisConnectionString = "localhost:6379";

        static async Task Main(string[] args)
        {
            try
            {
                var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
                var db = redis.GetDatabase();

                Console.WriteLine("Retrieving processed messages from Redis...");

                for (int i = 1; i <= 10; i++) // Adjust the range as needed
                {
                    var value = await db.StringGetAsync($"ProcessedMessage:{i}");
                    if (value.HasValue)
                    {
                        Console.WriteLine($"ProcessedMessage:{i} = {value}");
                    }
                }

                Console.WriteLine("Finished retrieving messages.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
