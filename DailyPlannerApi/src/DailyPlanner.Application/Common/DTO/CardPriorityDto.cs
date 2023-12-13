namespace DailyPlanner.Application.Common.DTO
{
    public record class CardPriorityDto
    {
        public int Index { get; init; }
        public string Value { get; init; }
    }
}