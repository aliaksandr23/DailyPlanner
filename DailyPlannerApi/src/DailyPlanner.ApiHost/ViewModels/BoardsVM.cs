using DailyPlanner.Application.Common.DTO;

namespace DailyPlanner.ApiHost.ViewModels
{
    public record class BoardsVM
    {
        public int BoardsRemaining { get; init; }
        public IReadOnlyCollection<BoardDto> Boards { get; init; }
    }
}