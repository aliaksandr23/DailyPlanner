using AutoMapper;
using DailyPlanner.Domain.Entities;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.CQRS.ToDoLists.Commands
{
    public record class CreateToDoListCommand : ICommand<ToDoListDto>
    {
        public Guid CardId { get; init; }
        public string Title { get; init; }
    }

    internal class CreateToDoListCommandHandler : ICommandHandler<CreateToDoListCommand, ToDoListDto>
    {
        private readonly IMapper _mapper;
        private readonly IToDoListRepository _toDoListRepository;

        public CreateToDoListCommandHandler(IMapper mapper, IToDoListRepository toDoListRepository)
        {
            _mapper = mapper;
            _toDoListRepository = toDoListRepository;
        }

        public async Task<ToDoListDto> Handle(CreateToDoListCommand request, CancellationToken cancellationToken)
        {
            var toDoListToAdd = _mapper.Map<ToDoList>(request);
            await _toDoListRepository
                .AddAsync(toDoListToAdd, cancellationToken);
            return _mapper.Map<ToDoListDto>(toDoListToAdd);
        }
    }
}