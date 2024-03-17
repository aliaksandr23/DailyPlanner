using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.Cards.Commands
{
    public record class UpdateCardCommand : ICommand<CardDto>
    {
        public Guid Id { get; init; }
        public Guid BoardId { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public bool? IsDone { get; init; }
        public string Priority { get; init; }
        public DateTime? EndDate { get; init; }
        public DateTime? StartDate { get; init; }
    }

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
                .GetCardByIdAsync(request.Id, request.BoardId, cancellationToken);
            _mapper.Map(request, cardToUpdate);
            await _cardRepository
                .UpdateAsync(cardToUpdate, cancellationToken);
            return _mapper.Map<CardDto>(cardToUpdate);
        }
    }
}