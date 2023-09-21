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
            
            CreateMap<Settlement, SettlementDtoViewModel>().ReverseMap();
        }
    }
}
