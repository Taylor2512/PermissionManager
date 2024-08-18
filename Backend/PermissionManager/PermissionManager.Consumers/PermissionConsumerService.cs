using Confluent.Kafka;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;
using PermissionManager.Core.Interfaces;
using PermissionManager.Shared;
using PermissionManager.Shared.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PermissionManager.Consumers
{
    public class PermissionConsumerService : IPermissionConsumerService
    {
        private readonly ILogger<PermissionConsumerService> _logger;
        private readonly IQueryPermissionUnitOfWork _elasticSearchService;
        private readonly IConfiguration _configuration;

        public PermissionConsumerService(
            ILogger<PermissionConsumerService> logger,
            IQueryPermissionUnitOfWork elasticSearchService,
            IConfiguration configuration)
        {
            _logger = logger;
            _elasticSearchService = elasticSearchService;
            _configuration = configuration;
        }

        public async Task ProcessMessagesAsync(CancellationToken stoppingToken)
        {
            var config = GetKafkaConsumerConfig();

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(nameof(Permission));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    var permissionEvent = JsonSerializer.Deserialize<MessageData<Permission>>(consumeResult.Message.Value);

                    if (permissionEvent != null)
                    {
                        await HandlePermissionEventAsync(permissionEvent);
                    }
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError($"Error occurred: {ex.Error.Reason}");
                }
            }
        }

        private ConsumerConfig GetKafkaConsumerConfig()
        {
            return new ConsumerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"],  // Asegúrate de que esto esté configurado correctamente
                GroupId  = this.GetType().Name + "-" + new Guid().ToString(),
                // El ID del grupo de consumidores
                AutoOffsetReset = AutoOffsetReset.Earliest,  // Empieza a leer desde el principio si no hay offset
                EnableAutoCommit = true,  // Activa el autocommit de offset
                AllowAutoCreateTopics = true,
                SecurityProtocol = SecurityProtocol.Plaintext,
            };
        }

        private async Task HandlePermissionEventAsync(MessageData<Permission> permissionEvent)
        {
            switch (permissionEvent.OperationType)
            {
                case "Create":
                    await _elasticSearchService.Permissions.CreateOrUpdateAsync(permissionEvent.Data);
                    _logger.LogInformation($"Permission with ID {permissionEvent.Data.Id} has been created in Elasticsearch.");
                    break;
                case "Update":
                    await _elasticSearchService.Permissions.CreateOrUpdateAsync(permissionEvent.Data);
                    _logger.LogInformation($"Permission with ID {permissionEvent.Data.Id} has been updated in Elasticsearch.");
                    break;
                case "Delete":
                    await _elasticSearchService.Permissions.DeleteAsync(permissionEvent.Data);
                    _logger.LogInformation($"Permission with ID {permissionEvent.Data.Id} has been deleted from Elasticsearch.");
                    break;
            }
        }

    }
}
