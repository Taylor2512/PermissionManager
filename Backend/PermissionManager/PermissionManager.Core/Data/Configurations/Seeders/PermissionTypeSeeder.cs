using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PermissionManager.Producter;
using PermissionManager.Shared;

namespace PermissionManager.Core.Data.Configurations.Seeders
{
    public class PermissionTypeSeeder : IEntityTypeConfiguration<PermissionType>
    {
        private readonly IProducerService _producerService;

        public PermissionTypeSeeder(IProducerService producerService)
        {
            _producerService = producerService;
        }

        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            var permissionTypes = new[]
            {
                new PermissionType { Id = 1, Name = "Read", Description = "Allows read-only access" },
                new PermissionType { Id = 2, Name = "Write", Description = "Allows writing and modifying data" },
                new PermissionType { Id = 3, Name = "Execute", Description = "Allows executing operations" },
                new PermissionType { Id = 4, Name = "Delete", Description = "Allows deletion of data" },
                new PermissionType { Id = 5, Name = "Admin", Description = "Grants full administrative access" }
            };

            builder.HasData(permissionTypes);

            // Enviar eventos a Kafka
            foreach (var permissionType in permissionTypes)
            {
                var eventMessage = new PermissionEvent<PermissionType>
                {
                    OperationType = "Create",
                    EventData = permissionType
                };
                _producerService.ProduceAsync(nameof(PermissionType), permissionType.Id.ToString(), eventMessage).Wait();
            }
        }
    }
}