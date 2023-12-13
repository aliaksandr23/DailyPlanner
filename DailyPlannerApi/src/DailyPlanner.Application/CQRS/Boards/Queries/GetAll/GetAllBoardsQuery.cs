using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Queries;

namespace DailyPlanner.Application.CQRS.Boards.Queries.GetAll
{
    public record class GetAllBoardsQuery : IQuery<IEnumerable<BoardDto>> { }
}