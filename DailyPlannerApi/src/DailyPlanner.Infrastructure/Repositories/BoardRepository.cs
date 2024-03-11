using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Data;
using DailyPlanner.Application.Common.DTO;
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
            return await DbSet.Where(b => b.Id == id).Select(b => new Board
            {
                Id = b.Id,
                Title = b.Title,
                IsPrivate = b.IsPrivate,
                IsFavorite = b.IsFavorite,
                CreatedOn = b.CreatedOn,
                CreatedBy = b.CreatedBy,
                UpdatedOn = b.UpdatedOn,
                UpdatedBy = b.UpdatedBy,
                Columns = b.Columns.Select(col => new Column
                {
                    Id = col.Id,
                    Title = col.Title,
                    BoardId = col.BoardId,
                    Cards = col.Cards.Select(card => new Card
                    {
                        Id = card.Id,
                        Title = card.Title,
                        EndDate = card.EndDate,
                        StartDate = card.StartDate,
                    })
                })
            }).FirstOrDefaultAsync(cancellationToken)
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