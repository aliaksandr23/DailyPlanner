namespace DailyPlanner.Application.Common.DTO
{
    public record class ColumnDto
    {
        public Guid Id { get; init; }
        public Guid BoardId { get; init; }
        public string Title { get; init; }
        public Guid CreatedBy { get; init; }
        public Guid? UpdatedBy { get; init; }
        public DateTime CreatedOn { get; init; }
        public DateTime? UpdatedOn { get; init; }
        public IEnumerable<CardDto> Cards { get; init; }
    }
}