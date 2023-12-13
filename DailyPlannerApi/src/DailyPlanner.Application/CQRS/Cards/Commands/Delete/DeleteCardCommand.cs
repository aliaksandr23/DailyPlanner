using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Cards.Commands.Delete
{
    public record class DeleteCardCommand : ICommand<CardDto>
    {
        public Guid Id { get; init; }
        public Guid ColumnId { get; init; }
    }
}