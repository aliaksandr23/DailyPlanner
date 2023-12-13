using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Columns.Commands.Delete
{
    internal class DeleteColumnCommandHandler : ICommandHandler<DeleteColumnCommand, ColumnDto>
    {
        private readonly IMapper _mapper;
        private readonly IColumnRepository _columnRepository;

        public DeleteColumnCommandHandler(IMapper mapper, IColumnRepository columnRepository)
        {
            _mapper = mapper;
            _columnRepository = columnRepository;
        }

        public async Task<ColumnDto> Handle(DeleteColumnCommand request, CancellationToken cancellationToken)
        {
            var columnToDelete = await _columnRepository
                .GetColumnByIdAsync(request.Id, request.BoardId, cancellationToken);
            await _columnRepository
                .DeleteAsync(columnToDelete, cancellationToken);
            return _mapper.Map<ColumnDto>(columnToDelete);
        }
    }
}