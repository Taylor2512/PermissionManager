using Confluent.Kafka;
using PermissionManager.Core.Data.UnitOfWork.Interfaces;
using PermissionManager.Shared;
using PermissionManager.Shared.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace PermissionManager.Consumers
{
    public class PermissionTypeConsumerService : IPermissionTypeConsumerService
    {
        private readonly ILogger<PermissionTypeConsumerService> _logger;
        private readonly IQueryPermissionUnitOfWork _elasticSearchService;
        private readonly IConfiguration _configuration;

        public PermissionTypeConsumerService(
            ILogger<PermissionTypeConsumerService> logger,
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
            consumer.Subscribe(nameof(PermissionType));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        IgnoreReadOnlyProperties = true,
                        IgnoreReadOnlyFields = true,
                        PropertyNameCaseInsensitive = true,
                    };
                    var permissionEvent = JsonSerializer.Deserialize<MessageData<PermissionType>>(consumeResult.Message.Value, options);

                    if (permissionEvent != null)
                    {
                        await HandlePermissionTypeEventAsync(permissionEvent);
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
                GroupId = this.GetType().Name+ "-"+new Guid().ToString(),
                AutoOffsetReset = AutoOffsetReset.Earliest,  // Empieza a leer desde el principio si no hay offset
                EnableAutoCommit = true,  // Activa el autocommit de offset
                AllowAutoCreateTopics = true,
                SecurityProtocol = SecurityProtocol.Plaintext,
            };
        }

        private async Task HandlePermissionTypeEventAsync(MessageData<PermissionType> permissionEvent)
        {
            switch (permissionEvent.OperationType)
            {
                case "Create":
                    await _elasticSearchService.PermissionTypeReadRepository.CreateOrUpdateAsync(permissionEvent.Data);
                    _logger.LogInformation($"Permission with ID {permissionEvent.Data.Id} has been created in Elasticsearch.");
                    break;
                case "Update":
                    await _elasticSearchService.PermissionTypeReadRepository.CreateOrUpdateAsync(permissionEvent.Data);
                    _logger.LogInformation($"Permission with ID {permissionEvent.Data.Id} has been updated in Elasticsearch.");
                    break;
                case "Delete":
                    await _elasticSearchService.PermissionTypeReadRepository.DeleteAsync(permissionEvent.Data);
                    _logger.LogInformation($"Permission with ID {permissionEvent.Data.Id} has been deleted from Elasticsearch.");
                    break;
                default:
                    await _elasticSearchService.PermissionTypeReadRepository.CreateOrUpdateAsync(permissionEvent.Data);
                    break;
            }
        }

    }
}
