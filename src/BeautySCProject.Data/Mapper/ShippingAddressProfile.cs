using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.AuthenticationModel;
using BeautySCProject.Data.Models.ShippingAddressModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Mapper
{
    public class ShippingAddressProfile : Profile
    {
        public ShippingAddressProfile()
        {
            CreateMap<ShippingAdressCreateRequest, ShippingAddress>();
            CreateMap<ShippingAdressUpdateRequest, ShippingAddress>();


      
        }
    }
}
