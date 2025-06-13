using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DeviceManagementAPI.Models
{
    public partial class DeviceServiceContext : DbContext
    {
        public DeviceServiceContext()
        {
        }

        public DeviceServiceContext(DbContextOptions<DeviceServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assignment> Assignments { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ViewAssignmentsPerDevice> ViewAssignmentsPerDevices { get; set; } = null!;
        public virtual DbSet<ViewServiceAssignment> ViewServiceAssignments { get; set; } = null!;
        public virtual DbSet<ViewUsedStatus> ViewUsedStatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=device_service_db;Username=postgres;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("Assignments_pkey");

                entity.HasOne(d => d.DeviceCodeNavigation)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.DeviceCode)
                    .HasConstraintName("Assignments_DeviceCode_fkey");

                entity.HasOne(d => d.ServiceCodeNavigation)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.ServiceCode)
                    .HasConstraintName("Assignments_ServiceCode_fkey");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.DeviceCode)
                    .HasName("Devices_pkey");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.ServiceCode)
                    .HasName("Services_pkey");
            });

            modelBuilder.Entity<ViewAssignmentsPerDevice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_AssignmentsPerDevice");
            });

            modelBuilder.Entity<ViewServiceAssignment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_ServiceAssignments");
            });

            modelBuilder.Entity<ViewUsedStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_UsedStatus");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
