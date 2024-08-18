using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PermissionManager.Core.Data.Repositories.Interfaces;
using PermissionManager.Producter;
using PermissionManager.Shared;

namespace PermissionManager.Core.Services
{
    public class CatalogSyncService : BackgroundService
    {
        private readonly ILogger<CatalogSyncService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _syncInterval = TimeSpan.FromMinutes(2);

        public CatalogSyncService(ILogger<CatalogSyncService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Starting catalog sync...");

                using (var scope = _serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IPermissionTypeWriteRepository>();
                    var producer = scope.ServiceProvider.GetRequiredService<IProducerService>();

                    var permissionTypes = await unitOfWork.GetAllAsync();
                    foreach (var permissionType in permissionTypes)
                    {
                        var mensaje= new MessageData<PermissionType>() { Data = permissionType };
                        await producer.ProduceAsync(permissionType.GetType().Name, permissionType.Id.ToString(), mensaje);
                    }
                }
                _logger.LogInformation("Catalog sync completed.");
                await Task.Delay(_syncInterval, stoppingToken);
            }
        }
    }
}
