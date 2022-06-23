using Microsoft.EntityFrameworkCore.Diagnostics;
using Pizza.Data.Entities;
using static Microsoft.EntityFrameworkCore.EntityState;

namespace Pizza.Data.Interceptors
{
    internal class SimpleAuditInterceptor : SaveChangesInterceptor
    {
        private SimpleAuditInterceptor() { }

        public static SimpleAuditInterceptor Instance { get; } = new();

        private static void SetCreateUpdateDates(DbContextEventData eventData)
        {
            if (eventData.Context != null)
            {
                var now = DateTime.UtcNow;
                foreach (var entityEntry in eventData.Context.ChangeTracker.Entries<EntityBase>())
                {
                    if (entityEntry.State == Added)
                        entityEntry.Entity.DateOfCreation = now;
                    if (entityEntry.State is Added or Modified)
                        entityEntry.Entity.DateOfUpdate = now;
                }
            }
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            SetCreateUpdateDates(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            SetCreateUpdateDates(eventData);
            return result;
        }
    }
}
