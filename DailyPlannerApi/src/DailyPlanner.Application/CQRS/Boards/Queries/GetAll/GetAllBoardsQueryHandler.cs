using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Queries;

namespace DailyPlanner.Application.CQRS.Boards.Queries.GetAll
{
    internal class GetAllBoardsQueryHandler : IQueryHandler<GetAllBoardsQuery, IEnumerable<BoardDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;

        public GetAllBoardsQueryHandler(IMapper mapper, IBoardRepository boardRepository)
        {
            _mapper = mapper;
            _boardRepository = boardRepository;
        }

        public async Task<IEnumerable<BoardDto>> Handle(GetAllBoardsQuery request, CancellationToken cancellationToken)
        {
            var boards = await _boardRepository
                .GetAllBoardsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BoardDto>>(boards);
        }
    }
}