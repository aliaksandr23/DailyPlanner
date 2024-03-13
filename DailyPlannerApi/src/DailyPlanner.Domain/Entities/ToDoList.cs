using DailyPlanner.Domain.Entities.Common;

namespace DailyPlanner.Domain.Entities;

public sealed class ToDoList : BaseAuditableEntity
{
    public string Title { get; set; }
    public Guid CardId { get; set; }
    public Card Card { get; set; }
    public IEnumerable<ToDoItem> Items { get; set; }
}