using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Mapper
{
    public class RoutineProfile:Profile
    {
        public RoutineProfile() 
        {
            CreateMap<Routine, RoutineViewModel>()
                .ForMember(dest => dest.RoutineDetails, opt => opt.MapFrom(src => src.RoutineDetails));

            CreateMap<RoutineDetail, RoutineDetailViewModel>()
                .ForMember(dest => dest.RoutineSteps, opt => opt.MapFrom(src => src.RoutineSteps));

            CreateMap<RoutineStep, RoutineStepViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<SkinType, SkinTypeViewModel>();
        }
    }
}
