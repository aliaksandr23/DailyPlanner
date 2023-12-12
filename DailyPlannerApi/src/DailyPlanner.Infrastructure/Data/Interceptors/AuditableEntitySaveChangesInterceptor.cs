using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using DailyPlanner.Infrastructure.Services.DateAndTime;
using DailyPlanner.Infrastructure.Services.CurrentUser;

namespace DailyPlanner.Infrastructure.Data.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _userService;
        private readonly IDateTimeService _dateTimeService;

        public AuditableEntitySaveChangesInterceptor(IDateTimeService dateTimeService, ICurrentUserService userService)
        {
            _userService = userService;
            _dateTimeService = dateTimeService;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            AddAuditableData(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AddAuditableData(DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _userService.UserId;
                        entry.Entity.CreatedOn = _dateTimeService.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _userService.UserId;
                        entry.Entity.UpdatedOn = _dateTimeService.Now;
                        break;
                }
            }
        }
    }
}