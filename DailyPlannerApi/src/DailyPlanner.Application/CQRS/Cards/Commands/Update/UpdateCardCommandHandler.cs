using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Cards.Commands.Update
{
    internal class UpdateCardCommandHandler : ICommandHandler<UpdateCardCommand, CardDto>
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;

        public UpdateCardCommandHandler(IMapper mapper, ICardRepository cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<CardDto> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            var cardToUpdate = await _cardRepository
                .GetCardByIdAsync(request.Id, request.ColumnId, cancellationToken);
            _mapper.Map(request, cardToUpdate);
            await _cardRepository
                .UpdateAsync(cardToUpdate, cancellationToken);
            return _mapper.Map<CardDto>(cardToUpdate);
        }
    }
}