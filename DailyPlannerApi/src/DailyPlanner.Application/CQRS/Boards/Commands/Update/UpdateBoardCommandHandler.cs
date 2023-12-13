using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Boards.Commands.Update
{
    internal class UpdateBoardCommandHandler : ICommandHandler<UpdateBoardCommand, BoardDto>
    {
        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;

        public UpdateBoardCommandHandler(IMapper mapper, IBoardRepository boardRepository)
        {
            _mapper = mapper;
            _boardRepository = boardRepository;
        }

        public async Task<BoardDto> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
        {
            var boardToUpdate = await _boardRepository
                .GetBoardByIdAsync(request.Id, cancellationToken);
            _mapper.Map(request, boardToUpdate);
            await _boardRepository
                .UpdateAsync(boardToUpdate, cancellationToken);
            return _mapper.Map<BoardDto>(boardToUpdate);
        }
    }
}