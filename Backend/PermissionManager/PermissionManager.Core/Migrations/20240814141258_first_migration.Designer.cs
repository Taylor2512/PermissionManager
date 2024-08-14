﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PermissionManager.Core.Models;

#nullable disable

namespace PermissionManager.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240814141258_first_migration")]
    partial class first_migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PermissionManager.Core.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateOnly>("PermissionDate")
                        .HasColumnType("date");

                    b.Property<int>("PermissionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Permissi__3214EC076E61CD05");

                    b.HasIndex(new[] { "PermissionTypeId" }, "IDX_Permissions_PermissionTypeId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("PermissionManager.Core.Models.PermissionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Permissi__3214EC070183A796");

                    b.ToTable("PermissionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Allows read-only access",
                            Name = "Read"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Allows writing and modifying data",
                            Name = "Write"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Allows executing operations",
                            Name = "Execute"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Allows deletion of data",
                            Name = "Delete"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Grants full administrative access",
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("PermissionManager.Core.Models.Permission", b =>
                {
                    b.HasOne("PermissionManager.Core.Models.PermissionType", "PermissionType")
                        .WithMany("Permissions")
                        .HasForeignKey("PermissionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Permissions_PermissionTypes");

                    b.Navigation("PermissionType");
                });

            modelBuilder.Entity("PermissionManager.Core.Models.PermissionType", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
