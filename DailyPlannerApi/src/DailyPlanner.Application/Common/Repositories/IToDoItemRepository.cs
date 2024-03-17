using DailyPlanner.Domain.Entities;

namespace DailyPlanner.Application.Common.Repositories;

public interface IToDoItemRepository : IBaseRepository<ToDoItem>
{
    Task<ToDoItem> GetFirstOrDefaultToDoItemAsync(Guid id, Guid listId, CancellationToken cancellationToken = default);
}