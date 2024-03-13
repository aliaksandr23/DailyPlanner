using DailyPlanner.Domain.Enums;
using DailyPlanner.Domain.Entities.Common;

namespace DailyPlanner.Domain.Entities;

public sealed class Card : BaseAuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public CardPriority Priority { get; set; }
    public CardDateSection CardDateSection { get; set; }
    public IEnumerable<ToDoList> ToDoLists { get; set; }
    public Guid ColumnId { get; set; }
    public Column Column { get; set; }
}