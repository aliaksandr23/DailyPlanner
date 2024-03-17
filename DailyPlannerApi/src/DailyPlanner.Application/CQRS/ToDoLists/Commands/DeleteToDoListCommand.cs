using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.ToDoLists.Commands
{
    public record class DeleteToDoListCommand : ICommand<ToDoListDto>
    {
        public Guid Id { get; init; }
        public Guid CardId { get; init; }
    }

    internal class DeleteToDoListCommandHandler : ICommandHandler<DeleteToDoListCommand, ToDoListDto>
    {
        private readonly IMapper _mapper;
        private readonly IToDoListRepository _toDoListRepository;

        public DeleteToDoListCommandHandler(IMapper mapper, IToDoListRepository toDoListRepository)
        {
            _mapper = mapper;
            _toDoListRepository = toDoListRepository;
        }

        public async Task<ToDoListDto> Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
        {
            var toDoListToDelete = await _toDoListRepository
                .GetFirstOrDefaultToDoListAsync(request.Id, request.CardId, cancellationToken);
            await _toDoListRepository
                .DeleteAsync(toDoListToDelete, cancellationToken);
            return _mapper.Map<ToDoListDto>(toDoListToDelete);
        }
    }
}