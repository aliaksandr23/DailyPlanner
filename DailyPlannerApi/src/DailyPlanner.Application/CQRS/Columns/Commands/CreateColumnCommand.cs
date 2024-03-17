using AutoMapper;
using DailyPlanner.Domain.Entities;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Columns.Commands
{
    public record class CreateColumnCommand : ICommand<ColumnDto>
    {
        public Guid BoardId { get; init; }
        public string Title { get; init; }
    }

    internal class CreateColumnCommandHandler : ICommandHandler<CreateColumnCommand, ColumnDto>
    {
        private readonly IMapper _mapper;
        private readonly IColumnRepository _columnRepository;

        public CreateColumnCommandHandler(IMapper mapper, IColumnRepository columnRepository)
        {
            _mapper = mapper;
            _columnRepository = columnRepository;
        }

        public async Task<ColumnDto> Handle(CreateColumnCommand request, CancellationToken cancellationToken)
        {
            var columnToAdd = _mapper.Map<Column>(request);
            await _columnRepository
                .AddAsync(columnToAdd, cancellationToken);
            return _mapper.Map<ColumnDto>(columnToAdd);
        }
    }
}