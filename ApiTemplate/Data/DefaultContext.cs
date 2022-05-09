using ApiTemplate.Entities;
using Common.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace ApiTemplate.Data
{
    public class DefaultContext : ApplicationDbContext
    {
        private readonly string _username;

        public DefaultContext(DbContextOptions<DefaultContext> options, IHttpContextAccessor httpContextAccessor) : base(ChangeOptionsType<ApplicationDbContext>(options))
        {
            var claimsPrincipal = httpContextAccessor.HttpContext?.User;
            _username = claimsPrincipal?.Claims?.SingleOrDefault(c => c.Type == "username")?.Value ?? "Unauthenticated user";
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // Get audit entries
            var auditEntries = OnBeforeSaveChanges(out List<PropertyEntry> tempProperties);

            // Save current entity
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            // Save audit entries
            await OnAfterSaveChangesAsync(auditEntries, tempProperties);
            return result;
        }

        private List<Audit> OnBeforeSaveChanges(out List<PropertyEntry> tempProperties)
        {
            tempProperties = new List<PropertyEntry>();
            ChangeTracker.DetectChanges();
            var entries = new List<Audit>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || entry.Entity is Audit || entry.Entity is AuditDetail)
                    continue;

                var auditEntry = new Audit
                {
                    AuditType = (byte)(entry.State == EntityState.Added ? AuditTypes.Insert : entry.State == EntityState.Deleted ? AuditTypes.Delete : AuditTypes.Update),
                    TableId = int.Parse(entry.Properties.Single(p => p.Metadata.IsPrimaryKey()).CurrentValue.ToString()),
                    TableName = entry.Metadata.ClrType.Name,
                    UserName = _username,
                    CreatedAt = DateTime.UtcNow,
                    AuditDetails = entry.Properties.Select(p => new AuditDetail
                    {
                        ColumnName = p.Metadata.Name,
                        NewValue = p.CurrentValue.ToString(),
                        OldValue = p.OriginalValue.ToString()
                    }).ToList(),

                };
                tempProperties = entry.Properties.Where(p => p.IsTemporary).ToList();
                entries.Add(auditEntry);
            }

            return entries;
        }

        private Task OnAfterSaveChangesAsync(List<Audit> auditEntries, List<PropertyEntry> tempProperties)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            // For each temporary property in each audit entry - update the value in the audit entry to the actual (generated) value
            foreach (var entry in auditEntries)
            {
                foreach (var prop in tempProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        entry.TableId = int.Parse(prop.CurrentValue.ToString());
                    }
                    entry.AuditDetails.Add(new AuditDetail
                    {
                        ColumnName = prop.Metadata.Name,
                        NewValue = prop.CurrentValue.ToString(),
                        OldValue = prop.OriginalValue.ToString()
                    });
                }
            }

            Audits.AddRange(auditEntries);
            return base.SaveChangesAsync();
        }

        protected static DbContextOptions<T> ChangeOptionsType<T>(DbContextOptions options) where T : DbContext
        {
            var sqlExt = options.Extensions.FirstOrDefault(e => e is SqlServerOptionsExtension);

            if (sqlExt == null)
                throw (new Exception("Failed to retrieve SQL connection string for base Context"));

            return new DbContextOptionsBuilder<T>()
                        .UseSqlServer(((SqlServerOptionsExtension)sqlExt).ConnectionString)
                        .Options;
        }
    }
}
