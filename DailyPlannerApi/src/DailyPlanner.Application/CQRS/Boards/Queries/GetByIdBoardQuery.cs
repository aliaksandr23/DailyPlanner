using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Queries;

namespace DailyPlanner.Application.CQRS.Boards.Queries
{
    public record class GetByIdBoardQuery : IQuery<BoardDto>
    {
        public Guid Id { get; init; }
    }

    internal class GetByIdBoardQueryHandler : IQueryHandler<GetByIdBoardQuery, BoardDto>
    {
        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;

        public GetByIdBoardQueryHandler(IMapper mapper, IBoardRepository boardRepository)
        {
            _mapper = mapper;
            _boardRepository = boardRepository;
        }

        public async Task<BoardDto> Handle(GetByIdBoardQuery request, CancellationToken cancellationToken)
        {
            var board = await _boardRepository
                .GetBoardByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<BoardDto>(board);
        }
    }
}