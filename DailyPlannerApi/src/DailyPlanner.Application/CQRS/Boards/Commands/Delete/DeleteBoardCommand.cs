using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Boards.Commands.Delete
{
    public record class DeleteBoardCommand : ICommand<BoardDto>
    {
        public Guid Id { get; init; }
    }
}