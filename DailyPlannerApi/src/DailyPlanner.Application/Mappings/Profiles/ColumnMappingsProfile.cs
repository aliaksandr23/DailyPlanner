using AutoMapper;
using DailyPlanner.Domain.Entities;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Columns.Commands.Create;
using DailyPlanner.Application.CQRS.Columns.Commands.Update;

namespace DailyPlanner.Application.Mappings.Profiles
{
    internal class ColumnMappingsProfile : Profile
    {
        public ColumnMappingsProfile()
        {
            CreateMap<Column, ColumnDto>();
            CreateMap<CreateColumnCommand, Column>().ValidateMemberList(MemberList.Source);
            CreateMap<UpdateColumnCommand, Column>().ValidateMemberList(MemberList.Source);
        }
    }
}