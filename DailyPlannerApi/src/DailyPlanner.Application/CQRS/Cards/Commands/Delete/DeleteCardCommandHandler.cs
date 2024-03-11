using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Cards.Commands.Delete
{
    internal class DeleteCardCommandHandler : ICommandHandler<DeleteCardCommand, CardDto>
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;

        public DeleteCardCommandHandler(IMapper mapper, ICardRepository cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<CardDto> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
        {
            var cardToDelete = await _cardRepository
                .GetCardByIdAsync(request.Id, request.BoardId, cancellationToken);
            await _cardRepository
                .DeleteAsync(cardToDelete, cancellationToken);
            return _mapper.Map<CardDto>(cardToDelete);
        }
    }
}