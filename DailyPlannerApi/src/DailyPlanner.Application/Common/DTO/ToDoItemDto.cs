namespace DailyPlanner.Application.Common.DTO;

public record class ToDoItemDto : BaseAuditableDtoEntity
{
    public string Title { get; init; }
    public bool IsDone { get; init; }
    public Guid ToDoListId { get; init; }
}