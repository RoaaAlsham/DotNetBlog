
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ZenBlog.Domain.Entities.Common;

namespace ZenBlog.Persistence.Intercepters
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    

    namespace ZenBlog.Persistence.Interceptors
    {
        public class AuditDbContextInterceptor : SaveChangesInterceptor 
        {
            private static readonly Dictionary<EntityState, Action<DbContext, BaseEntity>> Behaviors = new()
        {
            { EntityState.Added,    AddedBehavior    },
            { EntityState.Modified, ModifiedBehavior }
        };

            private static void AddedBehavior(DbContext context, BaseEntity entity)
            {
                context.Entry(entity).Property(x => x.UpdatedAt).IsModified = false; // suppress UpdatedAt on insert
                entity.CreatedAt = DateTime.UtcNow; 
            }

            private static void ModifiedBehavior(DbContext context, BaseEntity entity)
            {
                context.Entry(entity).Property(x => x.CreatedAt).IsModified = false; // lock CreatedAt on update
                entity.UpdatedAt = DateTime.UtcNow;
            }

            public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
                DbContextEventData eventData,
                InterceptionResult<int> result,
                CancellationToken cancellationToken = default)
            {
                foreach (var entry in eventData.Context!.ChangeTracker.Entries<BaseEntity>())// context is not null when this method is called, so we can safely use the null-forgiving operator
                {
                    if (Behaviors.TryGetValue(entry.State, out var behavior))
                    {
                        behavior(eventData.Context, entry.Entity);
                    }
                }

                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }
        }
    }
}
