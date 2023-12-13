using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Columns.Commands.Delete
{
    public record class DeleteColumnCommand : ICommand<ColumnDto>
    {
        public Guid Id { get; init; }
        public Guid BoardId { get; init; }
    }
}