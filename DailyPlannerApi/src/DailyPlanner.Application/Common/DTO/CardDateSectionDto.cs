namespace DailyPlanner.Application.Common.DTO;

public record class CardDateSectionDto
{
    public bool IsDone { get; init; }
    public DateTime? EndDate { get; init; }
    public DateTime? StartDate { get; init; }
}