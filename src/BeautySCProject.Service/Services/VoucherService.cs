using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data;
using BeautySCProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Repositories;

namespace BeautySCProject.Service.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<Voucher> GetVoucherByIdAsync(int voucherId)
        {
            return await _voucherRepository.GetVoucherByIdAsync(voucherId);
        }
    }
}
