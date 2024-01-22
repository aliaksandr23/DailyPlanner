using DailyPlanner.Domain.Enums;

namespace DailyPlanner.Domain.Entities
{
    public sealed class Card : BaseAuditableEntity
    {
        public bool IsDone { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public CardPriority Priority { get; set; }
        public Guid ColumnId { get; set; }
        public Column Column { get; set; }
    }
}