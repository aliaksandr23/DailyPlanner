using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Columns.Commands.Update
{
    public record class UpdateColumnCommand : ICommand<ColumnDto>
    {
        public Guid Id { get; init; }
        public Guid BoardId { get; init; }
        public string Title { get; init; }
    }
}