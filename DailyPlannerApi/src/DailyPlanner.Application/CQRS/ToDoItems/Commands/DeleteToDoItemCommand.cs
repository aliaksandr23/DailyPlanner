using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.ToDoItems.Commands
{
    public record class DeleteToDoItemCommand : ICommand<ToDoItemDto>
    {
        public Guid Id { get; init; }
        public Guid ToDoListId { get; init; }
    }

    internal class DeleteToDoItemCommandHandler : ICommandHandler<DeleteToDoItemCommand, ToDoItemDto>
    {
        private readonly IMapper _mapper;
        private readonly IToDoItemRepository _toDoItemRepository;

        public DeleteToDoItemCommandHandler(IMapper mapper, IToDoItemRepository toDoItemRepository)
        {
            _mapper = mapper;
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task<ToDoItemDto> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            var toDoItemToDelete = await _toDoItemRepository
                .GetFirstOrDefaultToDoItemAsync(request.Id, request.ToDoListId, cancellationToken);
            await _toDoItemRepository
                .DeleteAsync(toDoItemToDelete, cancellationToken);
            return _mapper.Map<ToDoItemDto>(toDoItemToDelete);
        }
    }
}