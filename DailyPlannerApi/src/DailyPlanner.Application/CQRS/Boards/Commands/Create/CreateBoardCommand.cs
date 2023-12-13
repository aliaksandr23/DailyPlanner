using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Boards.Commands.Create
{
    public record class CreateBoardCommand : ICommand<BoardDto>
    {
        public string Title { get; init; }
        public bool IsPrivate { get; init; }
        public bool IsFavorite { get; init; }
    }
}