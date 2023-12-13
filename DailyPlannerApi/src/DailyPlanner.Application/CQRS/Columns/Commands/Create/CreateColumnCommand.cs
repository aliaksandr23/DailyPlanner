using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Columns.Commands.Create
{
    public record class CreateColumnCommand : ICommand<ColumnDto>
    {
        public Guid BoardId { get; init; }
        public string Title { get; init; }
    }
}