using ApiTemplate.Entities;
using Common.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace ApiTemplate.Data;

public class ApplicationDbContext : BaseContext
{
    public ApplicationDbContext(DbContextOptions<BaseContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
