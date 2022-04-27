using ApiTemplate.Entities;
using Common.Base;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiTemplate.Data;

public class ApplicationDbContext : BaseContext
{
    public ApplicationDbContext(DbContextOptions<BaseContext> options, ILogger<BaseContext> logger)
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
