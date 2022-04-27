using Common.DTOs;
using Common.Extentions;
using Microsoft.EntityFrameworkCore;

namespace Common.Contract
{
    public class BaseContext : DbContext
    {
        private readonly ILogger _logger;

        public BaseContext(DbContextOptions<BaseContext> options,
            ILogger<BaseContext> logger) : base(options)
        {
            _logger = logger;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var changes = GetChanges().ToJson();

            _logger.LogInformation(changes);

            return await base.SaveChangesAsync(cancellationToken);
        }

        private List<DataChangeDto> GetChanges()
        {
            var result = new List<DataChangeDto>();
            foreach (var change in ChangeTracker.Entries()
                    .Where(a => a.State == EntityState.Modified))
            {
                var changes = new List<ChangesDto>();

                foreach (var prop in change.OriginalValues.Properties)
                {
                    var originalValue = change.OriginalValues[prop.Name];
                    var currentValue = change.CurrentValues[prop.Name];
                    if (!object.Equals(currentValue, originalValue))
                    {
                        changes.Add(new(prop.Name, originalValue, currentValue));
                    }
                }

                result.Add(new(change.Entity.GetType().Name, change.OriginalValues["Id"].ToString(), changes));
            }

            return result;
        }
    }
}
