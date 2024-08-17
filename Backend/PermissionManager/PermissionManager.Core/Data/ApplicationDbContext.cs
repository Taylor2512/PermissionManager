
using Microsoft.EntityFrameworkCore;

using PermissionManager.Core.Data.Configurations.Seeders;

using System;
using System.Collections.Generic;
using System.Reflection;
#nullable disable

namespace PermissionManager.Core.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<PermissionType> PermissionTypes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

}
