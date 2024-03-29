﻿using AutoMapper;
using WMKancelariapp.Extensions;
using WMKancelariapp.Models;
using WMKancelariapp.Models.ViewModels;

namespace WMKancelariapp.Data.Profiles
{
    public class TaskTypeProfile : Profile
    {
        public TaskTypeProfile()
        {
            CreateMap<TaskType, TaskTypeDtoViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<TaskTypeDtoViewModel, TaskType>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.TaskTypeId))
                .ForMember(x=>x.Name, opts=>opts.MapFrom(x=>x.Name.CapitaliseFirstLetter()));
            
            CreateMap<TaskType, TaskTypeDtoViewModel>()
                .ForMember(x=>x.TaskTypeId, opt=>opt.MapFrom(src => src.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(x => x.Name.CapitaliseFirstLetter()))
                .ReverseMap();
        }
    }
}
