namespace DailyPlanner.Application.Common.DTO;

public record class CardDto : BaseAuditableDtoEntity
{
    public string Title { get; init; }
    public string Priority { get; init; }
    public string Description { get; init; }
    public Guid ColumnId { get; init; }
    public ColumnDto Column { get; init; }
}