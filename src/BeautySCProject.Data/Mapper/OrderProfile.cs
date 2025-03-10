using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.OrderDetailModel;
using BeautySCProject.Data.Models.OrderModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCreateRequest, Order>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetailRequests))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Constants.ORDER_STATUS_PENDING))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));          
            CreateMap<PaymentMethod, PaymentMethodViewModel>();
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod.PaymentMethodName))
                .ForMember(dest => dest.Voucher, opt => opt.MapFrom(src => src.VoucherId == null ? null : new VoucherViewModel
                {
                    VoucherId = (int) src.VoucherId,
                    VoucherName = src.Voucher.VoucherName,
                    VoucherCode =src.Voucher.VoucherCode,
                    Description = src.Voucher.Description,
                    DiscountAmount = src.Voucher.DiscountAmount,
                    StartDate = src.Voucher.StartDate,
                    EndDate = src.Voucher.EndDate,
                    MinimumPurchase = src.Voucher.MinimumPurchase,
                    Status = src.Voucher.Status
                }))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.OrderDetails.Where(x => x.OrderId == src.OrderId).Select(od => new OrderDetailViewModel
                {
                    OrderDetailId = od.OrderDetailId,
                    ProductId = od.ProductId,
                    ProductName = od.Product.ProductName,
                    ProductImage = od.Product.ProductImages.Any() ? od.Product.ProductImages.FirstOrDefault().Url : "",
                    Size = od.Product.Size,
                    Quantity = od.Quantity,
                    Price = od.Product.Price,
                    Discount = od.Product.Discount,
                    Category = new CategoryViewModel
                    {
                        CategoryId = od.Product.CategoryId,
                        CategoryName = od.Product.Category.CategoryName
                    },
                    SkinTypes = od.Product.ProductSkinTypes.Select(pst => new SkinTypeViewModel
                    {
                        SkinTypeId = pst.SkinTypeId,
                        SkinTypeName = pst.SkinType.SkinTypeName
                    }).ToList()
                }).ToList()));
        }
    }
}
