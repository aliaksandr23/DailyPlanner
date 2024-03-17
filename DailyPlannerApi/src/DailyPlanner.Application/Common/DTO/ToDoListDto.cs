namespace DailyPlanner.Application.Common.DTO;

public record class ToDoListDto : BaseAuditableDtoEntity
{
    public string Title { get; init; }
    public Guid CardId { get; init; }
    public IEnumerable<ToDoItemDto> Items { get; init; }
}