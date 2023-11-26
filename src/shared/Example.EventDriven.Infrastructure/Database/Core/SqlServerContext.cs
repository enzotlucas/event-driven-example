using Example.EventDriven.Domain.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Example.EventDriven.Infrastructure.Database.Core
{
    public class SqlServerContext : DbContext
    {
        public DbSet<ProcessEntity> Processes { get; set; }

        public SqlServerContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
    }
}
