using AutoMapper;
using DailyPlanner.Domain.Enums;
using DailyPlanner.Domain.Entities;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Cards.Commands.Create;
using DailyPlanner.Application.CQRS.Cards.Commands.Update;

namespace DailyPlanner.Application.Mappings.Profiles
{
    internal class CardMappingsProfile : Profile
    {
        public CardMappingsProfile()
        {
            CreateMap<Card, CardDto>()
                .ForMember(dst => dst.Priority, opt => opt.MapFrom(src => src.Priority.ToString()));
            CreateMap<CreateCardCommand, Card>()
                .ForMember(dst => dst.Priority, opt => opt.MapFrom(src => Enum.Parse(typeof(CardPriority), src.Priority)))
                .ValidateMemberList(MemberList.Source);
            CreateMap<UpdateCardCommand, Card>()
                .ForMember(dst => dst.Priority, opt => opt.MapFrom(src => Enum.Parse(typeof(CardPriority), src.Priority)))
                .ValidateMemberList(MemberList.Source);
        }
    }
}   