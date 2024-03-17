using DailyPlanner.Domain.Entities.Common;

namespace DailyPlanner.Application.Common.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseAuditableEntity
{
    Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}