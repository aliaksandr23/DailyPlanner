using DailyPlanner.Domain.Entities;

namespace DailyPlanner.Application.Common.Repositories
{
    public interface IColumnRepository : IBaseRepository<Column> 
    {
        Task<Column> GetColumnByIdAsync(Guid id, Guid boardId, CancellationToken cancellationToken = default);
    }
}