using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.OrderDetailModel;
using BeautySCProject.Data.Models.ShippingAddressModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Mapper
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetailCreateRequest, OrderDetail>();

            CreateMap<Product, OrderDetail>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price * (1 - src.Discount)))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => 1));

            CreateMap<OrderDetail, CartViewModel>();
            CreateMap<OrderDetail, CartViewModel>();
        }
    }
}
