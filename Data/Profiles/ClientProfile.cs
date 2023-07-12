using AutoMapper;
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
            .ReverseMap();


        }

            
    }
}
