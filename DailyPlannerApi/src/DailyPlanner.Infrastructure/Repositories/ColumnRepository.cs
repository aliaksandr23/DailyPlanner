using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Data;
using DailyPlanner.Infrastructure.Exceptions;
using DailyPlanner.Infrastructure.Services.User;
using DailyPlanner.Application.Common.Repositories;

namespace DailyPlanner.Infrastructure.Repositories
{
    public class ColumnRepository : BaseRepository<Column>, IColumnRepository
    {
        public ColumnRepository(DailyPlannerDbContext context, IUserService userService)
            : base(context, userService) { }

        public async Task<Column> GetColumnByIdAsync(Guid id, Guid boardId, CancellationToken cancellationToken)
        {
            var column = await DbSet.FirstOrDefaultAsync(c => c.Id == id 
                && c.BoardId == boardId, cancellationToken);
            return column ?? throw new EntityNotFoundException(typeof(Column));
        }
    }
}