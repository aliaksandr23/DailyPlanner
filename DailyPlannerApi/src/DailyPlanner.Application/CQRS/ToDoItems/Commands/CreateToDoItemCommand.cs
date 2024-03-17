using AutoMapper;
using DailyPlanner.Domain.Entities;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.ToDoItems.Commands
{
    public record class CreateToDoItemCommand : ICommand<ToDoItemDto>
    {
        public Guid ToDoListId { get; init; }
        public string Title { get; init; }

    }

    internal class CreateToDoItemCommandHandler : ICommandHandler<CreateToDoItemCommand, ToDoItemDto>
    {
        private readonly IMapper _mapper;
        private readonly IToDoItemRepository _toDoItemRepository;

        public CreateToDoItemCommandHandler(IMapper mapper, IToDoItemRepository toDoItemRepository)
        {
            _mapper = mapper;
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task<ToDoItemDto> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var toDoItemToAdd = _mapper.Map<ToDoItem>(request);
            await _toDoItemRepository
                .AddAsync(toDoItemToAdd, cancellationToken);
            return _mapper.Map<ToDoItemDto>(toDoItemToAdd);
        }
    }
}