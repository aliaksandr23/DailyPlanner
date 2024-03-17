using AutoMapper;
using DailyPlanner.Domain.Entities;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.ToDoLists.Commands;

namespace DailyPlanner.Application.Mappings.Profiles;

internal class ToDoListMappingsProfile : Profile
{
    public ToDoListMappingsProfile()
    {
        CreateMap<ToDoList, ToDoListDto>();
        CreateMap<CreateToDoListCommand, ToDoList>().ValidateMemberList(MemberList.Source);
        CreateMap<UpdateToDoListCommand, ToDoList>().ValidateMemberList(MemberList.Source);
    }
}