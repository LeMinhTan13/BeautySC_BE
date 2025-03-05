using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.VoucherModel;
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
            CreateMap<VoucherViewModel, Voucher>();
            CreateMap<VoucherCreateRequestModel, Voucher>();
            CreateMap<VoucherUpdateRequestModel, Voucher>();
        }
    }
}
