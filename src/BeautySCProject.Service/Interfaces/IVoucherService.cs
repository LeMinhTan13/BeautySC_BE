using BeautySCProject.Data.Entities;
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
    }
}
