using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
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
    }
}
