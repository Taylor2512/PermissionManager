
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;

namespace PermissionManager.Core.Data.Configurations
{
    public partial class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC070183A796");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PermissionType> entity);
    }
}
