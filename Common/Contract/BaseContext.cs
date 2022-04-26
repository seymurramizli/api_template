using Microsoft.EntityFrameworkCore;

namespace Common.Contract
{
    public class BaseContext : DbContext
    {
        public BaseContext() { }

        public BaseContext(DbContextOptions<BaseContext> options)
      : base(options)
        {
        }
    }
}
