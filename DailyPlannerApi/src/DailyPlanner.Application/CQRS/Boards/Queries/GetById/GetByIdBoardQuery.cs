using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Queries;

namespace DailyPlanner.Application.CQRS.Boards.Queries.GetById
{
    public record class GetByIdBoardQuery : IQuery<BoardDto>
    {
        public Guid Id { get; init; }
    }
}