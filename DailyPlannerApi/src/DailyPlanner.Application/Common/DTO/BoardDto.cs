namespace DailyPlanner.Application.Common.DTO
{
    public record class BoardDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public bool IsPrivate { get; init; }
        public bool IsFavorite { get; init; }
        public Guid CreatedBy { get; init; }
        public Guid? UpdatedBy { get; init; }
        public DateTime CreatedOn { get; init; }
        public DateTime? UpdatedOn { get; init; }
        public IEnumerable<ColumnDto> Columns { get; init; }
    }
}