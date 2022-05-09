using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ApiTemplate.Entities;

namespace ApiTemplate.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Audit> Audits { get; set; } = null!;
        public virtual DbSet<AuditDetail> AuditDetails { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.TableName).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<AuditDetail>(entity =>
            {
                entity.Property(e => e.ColumnName).HasMaxLength(50);

                entity.Property(e => e.NewValue).HasMaxLength(500);

                entity.Property(e => e.OldValue).HasMaxLength(500);

                entity.HasOne(d => d.Audit)
                    .WithMany(p => p.AuditDetails)
                    .HasForeignKey(d => d.AuditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditDetails_ToAudits");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_ToDepartments");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
