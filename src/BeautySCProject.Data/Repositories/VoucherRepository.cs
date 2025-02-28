using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class VoucherRepository : Repository<Voucher>, IVoucherRepository
    {
        private readonly BeautyscDbContext _dbContext;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VoucherRepository(BeautyscDbContext dbContext, IUnitOfWork uow, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _uow = uow;
            _mapper = mapper;
        }
    
        public async Task<Voucher> GetVoucherByIdAsync(int voucherId)
        {
            return await Entities.FirstOrDefaultAsync(x => x.VoucherId == voucherId);
        }
        public async Task<IEnumerable<VoucherViewModel>> GetAllVoucherByCustomerIdAsync(int customerId)
        {
            var usedVoucherIds = await Entities
                               .Where(v => v.Orders.Any(o => o.CustomerId == customerId && o.VoucherId != null))
                               .Select(v => v.VoucherId)
                               .Distinct()
                               .ToListAsync();
            var availableVouchers = await Entities
                                .Where(v => !usedVoucherIds.Contains(v.VoucherId) && v.Status == true)
                                .Select(st => new VoucherViewModel
                                {
                                    VoucherId = st.VoucherId,
                                    VoucherName = st.VoucherName,
                                    VoucherCode = st.VoucherCode,
                                    Description = st.Description,
                                    DiscountAmount = st.DiscountAmount,
                                    StartDate = st.StartDate,
                                    EndDate = st.EndDate,
                                    MinimumPurchase = st.MinimumPurchase,
                                    Status = st.Status,
                                })
                                .ToListAsync();
            return availableVouchers;
        }
        public async Task<IEnumerable<VoucherViewModel>> GetAllVoucherAsync()
        {
             return await Entities
            .Select(st => new VoucherViewModel
            {
                VoucherId = st.VoucherId,
                VoucherName = st.VoucherName,
                VoucherCode = st.VoucherCode,
                Description = st.Description,
                DiscountAmount = st.DiscountAmount,
                EndDate = st.EndDate,
                MinimumPurchase = st.MinimumPurchase,
                StartDate = st.StartDate,
                Status = st.Status,
            })
            .ToListAsync();
        }
        public async Task<bool> SetStatusVoucherEqualFalse(Voucher voucher)
        {
            var existingVoucher = await Entities.FindAsync(voucher.VoucherId);
            if (existingVoucher == null)
            {
                return false; 
            }
            existingVoucher.VoucherName = voucher.VoucherName;
            existingVoucher.VoucherCode = voucher.VoucherCode;
            existingVoucher.Description = voucher.Description;
            existingVoucher.DiscountAmount = voucher.DiscountAmount;
            existingVoucher.StartDate = voucher.StartDate;
            existingVoucher.EndDate = voucher.EndDate;
            existingVoucher.MinimumPurchase = voucher.MinimumPurchase;
            existingVoucher.Status = false;
            Entities.Update(existingVoucher);
            await _uow.SaveChangesAsync();

            return true;
        }

    }
}
