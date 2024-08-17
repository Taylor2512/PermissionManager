using Confluent.Kafka;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using PermissionManager.Core.Interfaces;
using PermissionManager.Shared;
using System.Text.Json;

namespace PermissionManager.Consumer
{
    public class ConsumerService(ILogger<ConsumerService> logger, IElasticSearchService elasticSearchService, IConfiguration configuration) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                AllowAutoCreateTopics = true,
                SecurityProtocol = SecurityProtocol.Plaintext,
                ApiVersionRequest = true,
                Acks = Acks.All
            };
            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("permissions");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    var permissionEvent = JsonSerializer.Deserialize<PermissionEvent>(consumeResult.Message.Value);

                    if (permissionEvent != null)
                    {
                        switch (permissionEvent.OperationType)
                        {
                            case "Create":
                                await elasticSearchService.IndexPermissionAsync(permissionEvent.Permission);
                                logger.LogInformation($"Permission with ID {permissionEvent.Permission.Id} has been created in Elasticsearch.");
                                break;
                            case "Update":
                                await elasticSearchService.IndexPermissionAsync(permissionEvent.Permission);
                                logger.LogInformation($"Permission with ID {permissionEvent.Permission.Id} has been updated in Elasticsearch.");
                                break;
                            case "Delete":
                                await elasticSearchService.DeletePermissionAsync(permissionEvent.Permission);
                                logger.LogInformation($"Permission with ID {permissionEvent.Permission.Id} has been deleted from Elasticsearch.");
                                break;
                        }
                    }
                }
                catch (ConsumeException ex)
                {
                    logger.LogError($"Error occurred: {ex.Error.Reason}");
                }
            }
        }


       
    }

}
