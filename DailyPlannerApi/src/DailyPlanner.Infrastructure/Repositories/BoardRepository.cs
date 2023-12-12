using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Data;
using DailyPlanner.Infrastructure.Exceptions;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Infrastructure.Services.CurrentUser;

namespace DailyPlanner.Infrastructure.Repositories
{
    public class BoardRepository : BaseRepository<Board>, IBoardRepository
    {
        public BoardRepository(DailyPlannerDbContext context, ICurrentUserService userService)
            : base(context, userService) { }

        public async Task<Board> GetBoardByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var board = await DbSet.FirstOrDefaultAsync(b => b.Id == id
                && b.CreatedBy == _userService.UserId, cancellationToken);
            return board ?? throw new EntityNotFoundException(typeof(Board));
        }

        public async Task<IEnumerable<Board>> GetAllBoardsAsync(CancellationToken cancellationToken)
        {
            return await DbSet.Where(b => b.CreatedBy == _userService.UserId 
                && b.CreatedBy == _userService.UserId).ToListAsync(cancellationToken);
        }
    }
}