﻿using AutoMapper;
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
        }
    }
}
