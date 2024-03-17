using DailyPlanner.Domain.Entities;

namespace DailyPlanner.Application.Common.Repositories;

public interface IToDoListRepository : IBaseRepository<ToDoList>
{
    Task<ToDoList> GetFirstOrDefaultToDoListAsync(Guid id, Guid cardId, CancellationToken cancellationToken = default);
}