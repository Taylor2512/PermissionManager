using Confluent.Kafka;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using PermissionManager.Shared;

using System.Runtime.CompilerServices;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PermissionManager.Producter
{
    public interface IProducerService
    {
        Task ProduceAsync(string topic, string key, object message);

    }
    public class ProducerService : IProducerService
    {
        private readonly ILogger<ProducerService> _logger;
        private readonly IConfiguration _configuration;

        public ProducerService(ILogger<ProducerService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task ProduceAsync(string topic, string key, object message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"],
                AllowAutoCreateTopics = true,
                SecurityProtocol = SecurityProtocol.Plaintext,
                ApiVersionRequest = true,
                Acks = Acks.All
            };

            using var producer = new ProducerBuilder<string, string>(config).Build();

            try
            {
                var deliveryResult = await producer.ProduceAsync(topic, new Message<string, string>
                {
                    Key = key,
                    Value = JsonSerializer.Serialize(message)
                });

                _logger.LogInformation($"Delivered message to {deliveryResult.Topic}, Offset: {deliveryResult.Offset}");
            }
            catch (ProduceException<string, string> e)
            {
                _logger.LogError($"Delivery failed: {e.Error.Reason}");
            }

            producer.Flush();
        }
    }
}
