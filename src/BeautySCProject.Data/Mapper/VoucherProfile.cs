﻿using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Mapper
{
    public class VoucherProfile:Profile
    {
        public VoucherProfile()
        {
            CreateMap<Voucher, VoucherViewModel>();     
        }
    }
}
