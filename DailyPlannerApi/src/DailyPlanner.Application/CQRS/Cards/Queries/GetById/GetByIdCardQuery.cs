using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Queries;

namespace DailyPlanner.Application.CQRS.Cards.Queries.GetById
{
    public record class GetByIdCardQuery : IQuery<CardDto>
    {
        public Guid Id { get; init; }
        public Guid BoardId { get; init; }
    }
}