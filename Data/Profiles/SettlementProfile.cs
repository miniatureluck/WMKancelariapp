using AutoMapper;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Data.Profiles
{
    public class SettlementProfile : Profile
    {
        public SettlementProfile()
        {
            CreateMap<Case, CaseDtoViewModel>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<Settlement, SettlementDtoViewModel>()
                .ForMember(x=>x.AssignedUser, opt=>opt.MapFrom(x=>x.Client.AssignedUser))
                .ForMember(x=>x.SettlementId, opt=>opt.MapFrom(x=>x.Id))
                .ReverseMap();
        }
    }
}
