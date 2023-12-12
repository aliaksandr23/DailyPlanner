﻿using DailyPlanner.Domain.Entities;

namespace DailyPlanner.Application.Common.Repositories
{
    public interface ICardRepository : IBaseRepository<Card> 
    {
        Task<Card> GetCardByIdAsync(Guid id, Guid columnId, CancellationToken cancellationToken = default);
    }
}