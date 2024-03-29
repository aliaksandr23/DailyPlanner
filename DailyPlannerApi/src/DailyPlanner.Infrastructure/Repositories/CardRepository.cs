﻿using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Data;
using DailyPlanner.Infrastructure.Exceptions;
using DailyPlanner.Infrastructure.Services.User;
using DailyPlanner.Application.Common.Repositories;

namespace DailyPlanner.Infrastructure.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(DailyPlannerDbContext context, IUserService userService)
            : base(context, userService) { }

        public async Task<Card> GetCardByIdAsync(Guid id, Guid boardId, CancellationToken cancellationToken)
        {
            return await DbSet.Include(c => c.Column).Where(c => c.Id == id
            && c.Column.BoardId == boardId).Select(c => new Card
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                IsDone = c.IsDone,
                Priority = c.Priority,
                EndDate = c.EndDate,
                StartDate = c.StartDate,
                CreatedOn = c.CreatedOn,
                CreatedBy = c.CreatedBy,
                UpdatedOn = c.UpdatedOn,
                UpdatedBy = c.UpdatedBy,
                ColumnId = c.ColumnId,
                Column = c.Column,
            }).FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(typeof(Card));
        }
    }
}