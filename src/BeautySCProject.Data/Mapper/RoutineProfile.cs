using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.RoutineModel;
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
            CreateMap<RoutineCreateRequest, Routine>()
                .ForMember(dest => dest.RoutineDetails, opt => opt.MapFrom(src => src.RoutineDetails));

            CreateMap<RoutineDetailCreateRequest, RoutineDetail>()
                .ForMember(dest => dest.RoutineSteps, opt => opt.MapFrom(src => src.RoutineSteps));

            CreateMap<RoutineStepRequest, RoutineStep>();

            CreateMap<RoutineDetail, RoutineDetailCreateRequest>()
                .ForMember(dest => dest.RoutineSteps, opt => opt.MapFrom(src => src.RoutineSteps));

            CreateMap<RoutineStep, RoutineStepRequest>();
            CreateMap<Product, ProductRoutineViewModel>()
                .ForMember(dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImages));
            CreateMap<ProductImage, ProductImageRoutineViewModel>();// liên quan tới product nhưng chưa sửa vào product profile
        }
    }
}
