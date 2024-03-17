using AutoMapper;
using DailyPlanner.Domain.Entities;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.ToDoItems.Commands;

namespace DailyPlanner.Application.Mappings.Profiles;

internal class ToDoItemMappingsProfile : Profile
{
    public ToDoItemMappingsProfile()
    {
        CreateMap<ToDoItem, ToDoItemDto>();
        CreateMap<CreateToDoItemCommand, ToDoItem>().ValidateMemberList(MemberList.Source);
        CreateMap<UpdateToDoItemCommand, ToDoItem>().ValidateMemberList(MemberList.Source);
    }
}