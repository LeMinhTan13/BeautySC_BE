using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.AuthenticationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() {
            CreateMap<SignupRequest, Account>()
                  .ForMember(dest => dest.Password, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
                  .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Constants.USER_ROLE_CUSTOMER));
            CreateMap<SignupRequest, Customer>();
            //CreateMap<Customer, CustomerViewModel>();
        }
    }
}
