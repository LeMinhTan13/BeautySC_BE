using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IVoucherRepository
    {
        Task<Voucher> GetVoucherByIdAsync(int voucherId);
        Task<IEnumerable<VoucherViewModel>> GetAllVoucherAsync();
        Task<bool> SetStatusVoucherEqualFalse(Voucher voucher);
        Task<IEnumerable<VoucherViewModel>> GetAllVoucherByCustomerIdAsync(int customerId);
    }
}
