using Confluent.Kafka;

using Microsoft.Extensions.Logging;

using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Services;
using PermissionManager.Shared;

using System.Text.Json;

namespace PermissionManager.Consumers
{
    public class Worker: BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var kafkaConsumerService = scope.ServiceProvider.GetRequiredService<IKafkaConsumerService>();
                await kafkaConsumerService.ProcessMessagesAsync(stoppingToken);
            }
        }
    }
}
