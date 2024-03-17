using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Data;
using DailyPlanner.Infrastructure.Exceptions;
using DailyPlanner.Infrastructure.Services.User;
using DailyPlanner.Application.Common.Repositories;

namespace DailyPlanner.Infrastructure.Repositories;

public class ToDoItemRepository : BaseRepository<ToDoItem>, IToDoItemRepository
{
    public ToDoItemRepository(DailyPlannerDbContext context, IUserService userService) 
        : base(context, userService) { }

    public async Task<ToDoItem> GetFirstOrDefaultToDoItemAsync(Guid id, Guid listId, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(c => c.Id == id
        && c.ToDoListId == listId, cancellationToken)
        ?? throw new EntityNotFoundException(typeof(ToDoItem));
    }
}