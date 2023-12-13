using AutoMapper;
using DailyPlanner.Domain.Entities;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Cards.Commands.Create
{
    internal class CreateCardCommandHandler : ICommandHandler<CreateCardCommand, CardDto>
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;

        public CreateCardCommandHandler(IMapper mapper, ICardRepository cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<CardDto> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var cardToAdd = _mapper.Map<Card>(request);
            await _cardRepository
                .AddAsync(cardToAdd, cancellationToken);
            return _mapper.Map<CardDto>(cardToAdd);
        }
    }
}