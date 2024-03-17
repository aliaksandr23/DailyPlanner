using DailyPlanner.Domain.Entities;

namespace DailyPlanner.Application.Common.Repositories;

public interface IBoardRepository : IBaseRepository<Board> 
{
    Task<Board> GetBoardByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Board>> GetAllBoardsAsync(CancellationToken cancellationToken = default);
    Task<Board> GetFirstOrDefaultBoardAsync(Guid id, CancellationToken cancellationToken = default);
}