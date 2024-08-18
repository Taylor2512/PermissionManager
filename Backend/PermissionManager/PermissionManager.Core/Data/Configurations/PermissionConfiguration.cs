
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;

namespace PermissionManager.Core.Data.Configurations
{
    public partial class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC076E61CD05");
            entity.ToTable("permission");

            entity.HasIndex(e => e.PermissionTypeId, "IDX_Permissions_PermissionTypeId");

            entity.Property(e => e.EmployeeForename)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.EmployeeSurname)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.PermissionDate);

            entity.HasOne(d => d.PermissionType).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PermissionTypeId)
                .HasConstraintName("FK_Permissions_PermissionTypes");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Permission> entity);
    }
}
