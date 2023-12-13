using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Columns.Commands.Update
{
    internal class UpdateColumnCommandHandler : ICommandHandler<UpdateColumnCommand, ColumnDto>
    {
        private readonly IMapper _mapper;
        private readonly IColumnRepository _columnRepository;

        public UpdateColumnCommandHandler(IMapper mapper, IColumnRepository columnRepository)
        {
            _mapper = mapper;
            _columnRepository = columnRepository;
        }

        public async Task<ColumnDto> Handle(UpdateColumnCommand request, CancellationToken cancellationToken)
        {
            var columnToUpdate = await _columnRepository
                .GetColumnByIdAsync(request.Id, request.BoardId, cancellationToken);
            _mapper.Map(request, columnToUpdate);
            await _columnRepository
                .UpdateAsync(columnToUpdate, cancellationToken);
            return _mapper.Map<ColumnDto>(columnToUpdate);
        }
    }
}