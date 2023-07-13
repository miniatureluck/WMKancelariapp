using AutoMapper;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Data.Profiles
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            CreateMap<Case, CaseDtoViewModel>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Case, CaseDtoViewModel>()
                .ForMember(x=>x.CaseId, opt => opt.MapFrom(x=>x.Id))
                .ReverseMap();
        }
    }
}
