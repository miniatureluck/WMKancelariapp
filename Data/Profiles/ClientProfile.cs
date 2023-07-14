using AutoMapper;
using WMKancelariapp.Extensions;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Data.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
        CreateMap<Client, ClientDtoViewModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Client, ClientDtoViewModel>()
            .ForMember(x=> x.ClientId, opt => opt.MapFrom(x=>x.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name.ToTitleCase()))
            .ForMember(x => x.Surname, opt => opt.MapFrom(x => x.Surname.ToTitleCase()))
            .ReverseMap();


        }

            
    }
}
