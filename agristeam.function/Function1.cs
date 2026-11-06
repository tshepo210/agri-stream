using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using agristeam.function.Data;
using agristeam.function.Models;

namespace agristeam.function
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        private readonly StorageDbContext _dbContext;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Function1))]
        public void Run([QueueTrigger("irrigation-queue", Connection = "StorageConnection")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        }
    }
}
