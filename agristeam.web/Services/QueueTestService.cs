using Azure.Storage.Queues;
using System.Text;

namespace agristeam.web.Services
{
    public class QueueTestService
    {
        private readonly QueueClient _queueClient;

        public QueueTestService(IConfiguration configuration)
        {
            string connectionString =
                configuration.GetConnectionString("AzureStorage")
                ?? throw new InvalidOperationException(
                    "Azure Storage connection string is missing.");

            _queueClient = new QueueClient(
                connectionString,
                "orders-queue"
            );
        }

        public async Task SendTestOrderAsync()
        {
            await _queueClient.CreateIfNotExistsAsync();

            string order = """
            {
                "orderId": 1003,
                "product": "Laptop",
                "quantity": 2,
                "amount": 25000
            }
            """;

            await _queueClient.SendMessageAsync(
                Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(order)
                )
            );
        }
    }
}
