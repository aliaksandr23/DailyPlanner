using DailyPlanner.Domain.Entities;

namespace DailyPlanner.Application.Common.Repositories;

public interface IColumnRepository : IBaseRepository<Column> 
{
    Task<Column> GetFirstOrDefaultColumnAsync(Guid id, Guid boardId, CancellationToken cancellationToken = default);
}