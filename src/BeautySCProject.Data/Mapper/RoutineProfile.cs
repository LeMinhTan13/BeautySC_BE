using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.RoutineModel;
using BeautySCProject.Data.ViewModels;
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
            CreateMap<Routine, RoutineViewModelAdmin>()
                .ForMember(dest => dest.RoutineDetails, opt => opt.MapFrom(src => src.RoutineDetails));

            CreateMap<RoutineDetail, RoutineDetailViewModel>()
                .ForMember(dest => dest.RoutineSteps, opt => opt.MapFrom(src => src.RoutineSteps));
            CreateMap<RoutineDetail, RoutineDetailViewModelAdmin>()
                .ForMember(dest => dest.RoutineSteps, opt => opt.MapFrom(src => src.RoutineSteps));
            CreateMap<RoutineStep, RoutineStepViewModel>();
            CreateMap<RoutineStep, RoutineStepViewModelAdmin>();
            CreateMap<Category, CategoryRoutineViewModel>();
            CreateMap<Category, CategoryRoutineViewModelAdmin>();
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
                    opt => opt.MapFrom(src => src.ProductImages))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.ProductIngredients.Select(pi => pi.Ingredient)));
            CreateMap<ProductIngredient, IngredientViewModel>()
            .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.Ingredient.IngredientId))
            .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.IngredientName));
            CreateMap<ProductImage, ProductImageRoutineViewModel>();// liên quan tới product nhưng chưa sửa vào product profile
        }
    }
}
