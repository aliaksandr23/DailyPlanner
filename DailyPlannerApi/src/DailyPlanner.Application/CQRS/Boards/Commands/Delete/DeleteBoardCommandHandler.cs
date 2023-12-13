using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Boards.Commands.Delete
{
    internal class DeleteBoardCommandHandler : ICommandHandler<DeleteBoardCommand, BoardDto>
    {
        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;

        public DeleteBoardCommandHandler(IMapper mapper, IBoardRepository boardRepository)
        {
            _mapper = mapper;
            _boardRepository = boardRepository;
        }

        public async Task<BoardDto> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            var boardToDelete = await _boardRepository
                .GetBoardByIdAsync(request.Id, cancellationToken);
            await _boardRepository
                .DeleteAsync(boardToDelete, cancellationToken);
            return _mapper.Map<BoardDto>(boardToDelete);
        }
    }
}