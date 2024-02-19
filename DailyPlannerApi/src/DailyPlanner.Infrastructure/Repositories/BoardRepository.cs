using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Data;
using DailyPlanner.Infrastructure.Exceptions;
using DailyPlanner.Infrastructure.Services.User;
using DailyPlanner.Application.Common.Repositories;

namespace DailyPlanner.Infrastructure.Repositories
{
    public class BoardRepository : BaseRepository<Board>, IBoardRepository
    {
        public BoardRepository(DailyPlannerDbContext context, IUserService userService)
            : base(context, userService) { }

        public async Task<Board> GetBoardByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await DbSet
            .Include(b => b.Columns)
                .ThenInclude(c => c.Cards)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken)
            ?? throw new EntityNotFoundException(typeof(Board));
        }

        public async Task<IEnumerable<Board>> GetAllBoardsAsync(CancellationToken cancellationToken)
        {
            return await DbSet
            .Where(b => b.CreatedBy == _userService.UserId)
            .Select(b => new Board
            {
                Id = b.Id,
                Title = b.Title,
                IsFavorite = b.IsFavorite
            })
            .ToListAsync(cancellationToken);
        }
    }
}