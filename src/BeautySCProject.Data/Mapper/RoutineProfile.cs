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

            CreateMap<RoutineStepCreateRequest, RoutineStep>();
            CreateMap<RoutineStepUpdateRequest, RoutineStep>();
            CreateMap<RoutineDetail, RoutineDetailCreateRequest>()
                    .ForMember(dest => dest.RoutineSteps, opt => opt.MapFrom(src => src.RoutineSteps));

            CreateMap<RoutineStep, RoutineStepUpdateRequest>();
            CreateMap<RoutineStep, RoutineStepCreateRequest>();
            CreateMap<Product, ProductRoutineViewModel>()
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                    .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                    .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                    .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages))
                    .ForMember(dest => dest.ProductSkinTypes, opt => opt.MapFrom(src => src.ProductSkinTypes)) // Đúng kiểu dữ liệu
                    .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.ProductIngredients.Select(pi => pi.Ingredient)));

            CreateMap<ProductSkinType, ProductSkinTypeRoutineViewModel>();
            CreateMap<ProductIngredient, IngredientViewModel>()
                    .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.Ingredient.IngredientId))
                    .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.IngredientName));
            CreateMap<ProductImage, ProductImageRoutineViewModel>();// liên quan tới product nhưng chưa sửa vào product profile

            CreateMap<RoutineUpdateRequest, Routine>()
                    .ForMember(dest => dest.RoutineId, opt => opt.MapFrom(src => src.RoutineId))
                    .ForMember(dest => dest.RoutineName, opt => opt.MapFrom(src => src.RoutineName))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(dest => dest.SkinTypeId, opt => opt.MapFrom(src => src.SkinTypeId))
                    .ForMember(dest => dest.RoutineDetails, opt => opt.MapFrom(src => src.RoutineDetails));

            CreateMap<RoutineDetailUpdateRequest, RoutineDetail>()
                    .ForMember(dest => dest.RoutineDetailId, opt => opt.MapFrom(src => src.RoutineDetailId))
                    .ForMember(dest => dest.RoutineDetailName, opt => opt.MapFrom(src => src.RoutineDetailName))
                    .ForMember(dest => dest.RoutineSteps, opt => opt.MapFrom(src => src.RoutineSteps));
            CreateMap<RoutineStepUpdateRequest, RoutineStep>()
                    .ForMember(dest => dest.RoutineStepId, opt => opt.MapFrom(src => src.RoutineStepId))
                    .ForMember(dest => dest.Step, opt => opt.MapFrom(src => src.Step))
                    .ForMember(dest => dest.Instruction, opt => opt.MapFrom(src => src.Instruction))
                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
        }
    }
}
