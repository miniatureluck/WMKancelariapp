using AutoMapper;
using WMKancelariapp.Extensions;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Data.Profiles
{
    public class DeadlineProfile: Profile
    {
        public DeadlineProfile()
        {
            CreateMap<Deadline, DeadlineDtoViewModel>()
                .ForMember(x=>x.Description, opt=>opt.MapFrom(x=>x.Description.ToTitleCase()))
                .ForMember(x=>x.DeadlineId, opt=>opt.MapFrom(x=>x.Id))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<DeadlineDtoViewModel, Deadline>()
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description.ToTitleCase()))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.DeadlineId));
        }
    }
}
