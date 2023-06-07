using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using BasicRedisLeaderboardDemoDotNetCore.BLL.DbContexts.EntityConfigurations;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Rank> Companies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var item in ChangeTracker.Entries<IEntity>().AsEnumerable())
            {
                //Auto Timestamp
                if(item.State == EntityState.Modified)
                {
                    item.Entity.UpdatedAt = DateTime.Now;
                }
                else
                {
                    item.Entity.CreatedAt = DateTime.Now;
                    item.Entity.UpdatedAt = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
        }
    }
}