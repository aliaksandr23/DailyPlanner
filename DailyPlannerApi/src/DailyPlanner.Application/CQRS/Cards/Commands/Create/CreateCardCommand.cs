using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Cards.Commands.Create
{
    public record class CreateCardCommand : ICommand<CardDto>
    {
        public Guid ColumnId { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public CardPriorityDto Priority { get; init; }
        public DateTime? EndDate { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? RemindDate { get; init; }
    }
}