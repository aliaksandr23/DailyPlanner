using AutoMapper;
using DailyPlanner.Domain.Entities;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Boards.Commands
{
    public record class CreateBoardCommand : ICommand<BoardDto>
    {
        public string Title { get; init; }
        public bool IsPrivate { get; init; }
    }

    internal class CreateBoardCommandHandler : ICommandHandler<CreateBoardCommand, BoardDto>
    {
        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;

        public CreateBoardCommandHandler(IMapper mapper, IBoardRepository boardRepository)
        {
            _mapper = mapper;
            _boardRepository = boardRepository;
        }

        public async Task<BoardDto> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            var boardToAdd = _mapper.Map<Board>(request);
            await _boardRepository
                .AddAsync(boardToAdd, cancellationToken);
            return _mapper.Map<BoardDto>(boardToAdd);
        }
    }
}