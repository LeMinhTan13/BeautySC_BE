using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BeautySCProject.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly BeautyscDbContext _context;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TransactionRepository(BeautyscDbContext context, IUnitOfWork uow, IMapper mapper) : base(context)
        {
            _context = context;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionViewModel>> GetAllTransactionAsync(DateTime? startDate, DateTime? endDate, int pageSize, int pageIndex)
        {
            if (pageSize != 8 && pageSize != 10 && pageSize != 12) pageSize = 20;
            if (pageIndex < 1) pageIndex = 1;

            return await Entities.Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                                 .Skip(pageSize * (pageIndex-1)).Take(pageSize).Select(x => _mapper.Map<TransactionViewModel>(x)).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionByCustomerIdAsync(int customerId)
        {
            return await Entities.Include(x => x.Order).Where(x => x.Order.CustomerId == customerId).ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int transactionId)
        {
            return await Entities.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
        }

        public async Task<bool> CreateTransactionAsync(Transaction transaction)
        {
            try
            {
                await Entities.AddAsync(transaction);                
                await _uow.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return await Task.FromResult(false);
            }
        }
    }
}
    