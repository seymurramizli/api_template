﻿using ApiTemplate.Entities;
using Common.Contract;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiTemplate.Data;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(DbContextOptions<BaseDbContext> options, ILogger<BaseDbContext> logger)
        : base(options, logger)
    {
        
    } 

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
