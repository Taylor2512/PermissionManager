using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Consumers
{
    internal class PermissionTypeJob(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var kafkaConsumerService = scope.ServiceProvider.GetRequiredService<IPermissionConsumerService>();
                await kafkaConsumerService.ProcessMessagesAsync(stoppingToken);
            }
        }
    }
}
