using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IVoucherService
    {
        Task<Voucher> GetVoucherByIdAsync(int voucherId);
        Task<MethodResult<VoucherViewModel>> GetVoucherDetailByIdAsync(int voucherId);
        Task<MethodResult<IEnumerable<VoucherViewModel>>> GetAllVoucherAsync();
        Task<MethodResult<String>> SetStatusVoucherEqualFalse(Voucher voucher);
        Task<MethodResult<IEnumerable<VoucherViewModel>>> GetAllVoucherByCustomerIdAsync(int customerId);
    }
}
