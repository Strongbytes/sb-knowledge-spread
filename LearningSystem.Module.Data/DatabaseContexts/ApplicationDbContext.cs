using LearningSystem.Module.Data.Models;
using LearningSystem.Module.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearningSystem.Module.Data.DatabaseContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DateTime now = DateTime.UtcNow;
            foreach (EntityEntry changedEntity in ChangeTracker.Entries())
            {
                if (!(changedEntity.Entity is BaseDbEntity entity))
                {
                    continue;
                }

                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        entity.CreatedOn = now;
                        entity.LastModifiedOn = now;
                        break;
                    case EntityState.Modified:
                        Entry(entity).Property(x => x.CreatedOn).IsModified = false;
                        entity.LastModifiedOn = now;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<LearningPath> LearningPaths { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Tutorial> Tutorials { get; set; }
    }
}