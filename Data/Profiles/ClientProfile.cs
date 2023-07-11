using AutoMapper;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Data.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
        CreateMap<Client, CreateClientViewModel>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Client, CreateClientViewModel>().ReverseMap();
        }

            
    }
}
