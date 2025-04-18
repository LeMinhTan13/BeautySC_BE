﻿using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.AuthenticationModel;
using BeautySCProject.Data.ViewModels;
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
            CreateMap<Customer, ProfileViewModel>()
                .ForMember(dest => dest.SkinType, opt => opt.MapFrom(src => src.SkinTypeId == null ? null : new SkinTypeViewModel
                {
                    SkinTypeId = (int) src.SkinTypeId,
                    SkinTypeName = src.SkinType.SkinTypeName
                }));
            CreateMap<Customer, CustomerViewModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.SkinType, opt => opt.MapFrom(src => src.SkinTypeId == null ? null : new SkinTypeViewModel
                {
                    SkinTypeId = (int)src.SkinTypeId,
                    SkinTypeName = src.SkinType.SkinTypeName
                }));
        }
    }
}
