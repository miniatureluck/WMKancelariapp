using AutoMapper;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Data.Profiles
{
    public class HourlyPriceProfile : Profile
    {
        public HourlyPriceProfile()
        {
            CreateMap<HourlyPrice, HourlyPriceDtoViewModel>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<HourlyPrice, HourlyPriceDtoViewModel>()
                .ForMember(src => src.HourlyPriceId, opt => opt.MapFrom(dest => dest.Id))
                .ReverseMap();
        }
    }
}
