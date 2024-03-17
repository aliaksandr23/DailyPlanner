namespace DailyPlanner.Domain.Entities;

public sealed class CardDateSection
{
    public bool IsDone { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? StartDate { get; set; }
}