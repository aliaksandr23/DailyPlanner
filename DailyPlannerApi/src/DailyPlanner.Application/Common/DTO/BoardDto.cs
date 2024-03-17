namespace DailyPlanner.Application.Common.DTO;

public record class BoardDto : BaseAuditableDtoEntity
{
    public string Title { get; init; }
    public bool IsPrivate { get; init; }
    public bool IsFavorite { get; init; }
    public IEnumerable<ColumnDto> Columns { get; init; }
}