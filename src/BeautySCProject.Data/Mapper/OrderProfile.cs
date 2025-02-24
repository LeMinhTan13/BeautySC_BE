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
        }
    }
}
