using AutoMapper;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Data.Profiles
{
    public class UserTaskProfile : Profile
    {
        public UserTaskProfile()
        {
            CreateMap<UserTask, UserTaskDtoViewModel>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UserTask, UserTaskDtoViewModel>()
                .ForMember(x => x.UserTaskId, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();

            CreateMap<TaskType, TaskTypeDtoViewModel>()
                .ReverseMap();
        }
    }
}
