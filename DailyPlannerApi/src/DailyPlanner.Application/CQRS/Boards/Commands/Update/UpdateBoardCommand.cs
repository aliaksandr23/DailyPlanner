using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Boards.Commands.Update
{
    public record class UpdateBoardCommand : ICommand<BoardDto>
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public bool IsPrivate { get; init; }
        public bool IsFavorite { get; init; }
    }
}