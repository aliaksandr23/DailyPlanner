namespace DailyPlanner.Application.Common.DTO;

public record class ColumnDto : BaseAuditableDtoEntity
{
    public string Title { get; init; }
    public Guid BoardId { get; init; }
    public IEnumerable<CardDto> Cards { get; init; }
}