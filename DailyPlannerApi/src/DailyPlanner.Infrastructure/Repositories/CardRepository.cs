using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Data;
using DailyPlanner.Infrastructure.Exceptions;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Infrastructure.Services.CurrentUser;

namespace DailyPlanner.Infrastructure.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(DailyPlannerDbContext context, ICurrentUserService userService)
            : base(context, userService) { }

        public async Task<Card> GetCardByIdAsync(Guid id, Guid columnId, CancellationToken cancellationToken)
        {
            var card = await DbSet.FirstOrDefaultAsync(c => c.Id == id 
                && c.ColumnId == columnId, cancellationToken);
            return card ?? throw new EntityNotFoundException(typeof(Card));
        }
    }
}