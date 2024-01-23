using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Data;
using DailyPlanner.Infrastructure.Services.User;
using DailyPlanner.Application.Common.Repositories;

namespace DailyPlanner.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseAuditableEntity
    {
        protected DbSet<TEntity> DbSet { get; init; }
        protected DailyPlannerDbContext Context { get; init; }
        protected readonly IUserService _userService;

        public BaseRepository(DailyPlannerDbContext context, IUserService userService)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
            _userService = userService;
        }

        public async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            DbSet.Remove(entity);
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            DbSet.Update(entity);
            return await Context.SaveChangesAsync(cancellationToken);
        }
    }
}