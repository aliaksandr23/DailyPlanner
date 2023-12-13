using AutoMapper;
using DailyPlanner.Domain.Entities;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Boards.Commands.Create;
using DailyPlanner.Application.CQRS.Boards.Commands.Update;

namespace DailyPlanner.Application.Mappings.Profiles
{
    internal class BoardMappingsProfile : Profile
    {
        public BoardMappingsProfile()
        {
            CreateMap<Board, BoardDto>();
            CreateMap<CreateBoardCommand, Board>().ValidateMemberList(MemberList.Source);
            CreateMap<UpdateBoardCommand, Board>().ValidateMemberList(MemberList.Source);
        }
    }
}