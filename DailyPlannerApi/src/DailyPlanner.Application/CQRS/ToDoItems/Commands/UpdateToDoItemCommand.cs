using AutoMapper;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.ToDoItems.Commands
{
    public record class UpdateToDoItemCommand : ICommand<ToDoItemDto>
    {
        public Guid Id { get; init; }
        public Guid ToDoListId { get; init; }
        public string Title { get; init; }
        public bool IsDone { get; init; }
    }

    internal class UpdateToDoItemCommandHandler : ICommandHandler<UpdateToDoItemCommand, ToDoItemDto>
    {
        private readonly IMapper _mapper;
        private readonly IToDoItemRepository _toDoItemRepository;

        public UpdateToDoItemCommandHandler(IMapper mapper, IToDoItemRepository toDoItemRepository)
        {
            _mapper = mapper;
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task<ToDoItemDto> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var toDoItemToUpdate = await _toDoItemRepository
                .GetFirstOrDefaultToDoItemAsync(request.Id, request.ToDoListId, cancellationToken);
            _mapper.Map(request, toDoItemToUpdate);
            await _toDoItemRepository
                .UpdateAsync(toDoItemToUpdate, cancellationToken);
            return _mapper.Map<ToDoItemDto>(toDoItemToUpdate);
        }
    }
}