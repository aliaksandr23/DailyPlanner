using DailyPlanner.Domain.Entities.Common;

namespace DailyPlanner.Domain.Entities;

public sealed class ToDoItem : BaseAuditableEntity
{
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public Guid ToDoListId { get; set; }
    public ToDoList ToDoList { get; set; }
}
