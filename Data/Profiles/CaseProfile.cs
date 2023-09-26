using AutoMapper;
using WMKancelariapp.Extensions;
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
                .ForMember(x => x.CaseId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x=>x.Description, opt=>opt.MapFrom(x=>x.Description.CapitaliseFirstLetter()))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name.CapitaliseFirstLetter()));

            CreateMap<CaseDtoViewModel, Case>()
                .ForMember(x=>x.Id, opt=>opt.MapFrom(x=>x.CaseId))
                .ForMember(x=>x.Description, opt=>opt.MapFrom(x=>x.Description.CapitaliseFirstLetter()))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name.CapitaliseFirstLetter()));
        }
    }
}
