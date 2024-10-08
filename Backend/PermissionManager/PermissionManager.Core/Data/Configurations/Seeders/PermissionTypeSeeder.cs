﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PermissionManager.Producter;
using PermissionManager.Shared;

namespace PermissionManager.Core.Data.Configurations.Seeders
{
    public class PermissionTypeSeeder : IEntityTypeConfiguration<PermissionType>
    {


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

        }
    }
}