using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Cards.Commands.Update
{
    public record class UpdateCardCommand : ICommand<CardDto>
    {
        public Guid Id { get; init; }
        public Guid ColumnId { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public bool IsDone { get; init; }
        public string Priority { get; init; }
        public DateTime? EndDate { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? RemindDate { get; init; }
    }
}