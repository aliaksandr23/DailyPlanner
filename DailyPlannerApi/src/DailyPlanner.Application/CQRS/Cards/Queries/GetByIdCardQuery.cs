using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Queries;

namespace DailyPlanner.Application.CQRS.Cards.Queries
{
    public record class GetByIdCardQuery : IQuery<CardDto>
    {
        public Guid Id { get; init; }
        public Guid BoardId { get; init; }
    }

    internal class GetByIdCardQueryHandler : IQueryHandler<GetByIdCardQuery, CardDto>
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;

        public GetByIdCardQueryHandler(IMapper mapper, ICardRepository cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<CardDto> Handle(GetByIdCardQuery request, CancellationToken cancellationToken)
        {
            var card = await _cardRepository
                .GetCardByIdAsync(request.Id, request.BoardId, cancellationToken);
            return _mapper.Map<CardDto>(card);
        }
    }
}