namespace DailyPlanner.Application.Common.DTO
{
    public record class CardDto
    {
        public Guid Id { get; init; }
        public Guid ColumnId { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public bool IsDone { get; init; }
        public Guid CreatedBy { get; init; }
        public Guid? UpdatedBy { get; init; }
        public DateTime CreatedOn { get; init; }
        public DateTime? UpdatedOn { get; init; }
        public DateTime? EndDate { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? RemindDate { get; init; }
        public CardPriorityDto Priority { get; init; }
        public ColumnDto Column { get; init; }
    }
}