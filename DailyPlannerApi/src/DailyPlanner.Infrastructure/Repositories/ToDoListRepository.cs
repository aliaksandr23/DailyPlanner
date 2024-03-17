using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Data;
using DailyPlanner.Infrastructure.Exceptions;
using DailyPlanner.Infrastructure.Services.User;
using DailyPlanner.Application.Common.Repositories;

namespace DailyPlanner.Infrastructure.Repositories;

public class ToDoListRepository : BaseRepository<ToDoList>, IToDoListRepository
{
    public ToDoListRepository(DailyPlannerDbContext context, IUserService userService)
        : base(context, userService) { }

    public async Task<ToDoList> GetFirstOrDefaultToDoListAsync(Guid id, Guid cardId, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(c => c.Id == id
        && c.CardId == cardId, cancellationToken)
        ?? throw new EntityNotFoundException(typeof(ToDoList));
    }
}