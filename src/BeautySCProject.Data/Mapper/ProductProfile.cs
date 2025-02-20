using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.OrderModel;
using BeautySCProject.Data.Models.ProductModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateRequest, Product>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1));

            CreateMap<ProductUpdateRequest, Product>();
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Feedbacks.Select(x => x.Rating).Average()))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault().Url));
            //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductVersions.FirstOrDefault().ProductName))
            //.ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.ProductVersions.FirstOrDefault().Summary))
            //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductVersions.Min(x => x.Price * (decimal)(1 - x.Discount))));
            CreateMap<Product, ProductDetailViewModel>()
                //.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductVersions.FirstOrDefault().ProductName))
                //.ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.ProductVersions.FirstOrDefault().Summary))
                //.ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.ProductVersions.Select(x => new ProductSku
                //{
                //    ProductVersionId = x.ProductVersionId,
                //    Size = x.Sku.ToLower(),
                //    Price = x.Price,
                //    Discount = x.Discount,
                //    Quantity = x.Quantity,
                //    Status = x.Status
                //})))
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(x => new ProductImageViewModel
                {
                    ProductImageId = x.ProductImageId,
                    ProductImage = x.Url,
                })))
                .ForMember(dest => dest.SkinTypes, opt => opt.MapFrom(src => src.ProductSkinTypes.Select(x => new SkinTypeViewModel
                {
                    SkinTypeId = x.SkinTypeId,
                    SkinTypeName = x.SkinType.SkinTypeName,
                })))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => new BrandViewModel
                {
                    BrandId = src.BrandId,
                    BrandName = src.Brand.BrandName
                }))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new CategoryViewModel
                {
                    CategoryId = src.CategoryId,
                    CategoryName = src.Category.CategoryName
                }))
                .ForMember(dest => dest.Functions, opt => opt.MapFrom(src => src.ProductFunctions.Select(x => new FunctionViewModel
                {
                    FunctionId = x.FunctionId,
                    FunctionName = x.Function.FunctionName,
                })))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.ProductIngredients.Select(x => new IngredientViewModel
                 {
                     IngredientId = x.IngredientId,
                     IngredientName = x.Ingredient.IngredientName,
                 })))
                .ForMember(dest => dest.Feedbacks, opt => opt.MapFrom(src => src.Feedbacks.Select(x => new FeedbackViewModel
                {
                    FeedbackId = x.FeedbackId,
                    Rating = x.Rating,
                    Comment = x.Comment,
                    CreatedDate = x.CreatedDate
                })));
        }
    }
}
