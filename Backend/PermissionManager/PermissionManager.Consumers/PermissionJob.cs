using Confluent.Kafka;

using Microsoft.Extensions.Logging;

using PermissionManager.Core.Interfaces;
using PermissionManager.Core.Services;
using PermissionManager.Shared;

using System.Text.Json;

namespace PermissionManager.Consumers
{
    public class PermissionJob: BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public PermissionJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var kafkaConsumerService = scope.ServiceProvider.GetRequiredService<IPermissionConsumerService>();
                await kafkaConsumerService.ProcessMessagesAsync(stoppingToken);
            }
        }
    }
}
