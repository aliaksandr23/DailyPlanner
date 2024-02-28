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
            CreateMap<UpdateBoardCommand, Board>()
                .ForMember(dest => dest.IsPrivate, opt => opt.Condition(src => src.IsPrivate is not null))
                .ForMember(dest => dest.IsFavorite, opt => opt.Condition(src => src.IsFavorite is not null))
                .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title is not null))
                .ValidateMemberList(MemberList.Source);

        }
    }
}