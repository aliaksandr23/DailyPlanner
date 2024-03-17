using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.ToDoLists.Commands
{
    public record class UpdateToDoListCommand : ICommand<ToDoListDto>
    {
        public Guid Id { get; init; }
        public Guid CardId { get; init; }
        public string Title { get; init; }
    }

    internal class UpdateToDoListCommandHandler : ICommandHandler<UpdateToDoListCommand, ToDoListDto>
    {
        private readonly IMapper _mapper;
        private readonly IToDoListRepository _toDoListRepository;

        public UpdateToDoListCommandHandler(IMapper mapper, IToDoListRepository toDoListRepository)
        {
            _mapper = mapper;
            _toDoListRepository = toDoListRepository;
        }

        public async Task<ToDoListDto> Handle(UpdateToDoListCommand request, CancellationToken cancellationToken)
        {
            var toDoListToUpdate = await _toDoListRepository
                .GetFirstOrDefaultToDoListAsync(request.Id, request.CardId, cancellationToken);
            _mapper.Map(request, toDoListToUpdate);
            await _toDoListRepository
                .UpdateAsync(toDoListToUpdate, cancellationToken);
            return _mapper.Map<ToDoListDto>(toDoListToUpdate);
        }
    }
}