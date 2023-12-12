namespace DailyPlanner.Domain.Entities
{
    public sealed class Board : BaseAuditableEntity
    {
        public string Title { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsFavorite { get; set; }
        public IEnumerable<Column> Columns { get; set; }
    }
}