using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Service.Interfaces;


namespace BeautySCProject.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ICustomerService _userService;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, ICustomerService userService)
        {
            _transactionRepository = transactionRepository;            
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<MethodResult<IEnumerable<TransactionViewModel>>> GetAllTransactionAsync(DateTime? startDate, DateTime? endDate, int pageSize, int pageIndex)
        {
            {
                var result = await _transactionRepository.GetAllTransactionAsync(startDate, endDate, pageSize, pageIndex);
                return new MethodResult<IEnumerable<TransactionViewModel>>.Success(result);
            }
        }

        public async Task<MethodResult<List<TransactionViewModel>>> GetTransactionByCustomerAsync(string email)
            => await _userService.GetByEmailAsync(email).Result.Bind<List<TransactionViewModel>>(async user =>
            {
                var result = await _transactionRepository.GetTransactionByCustomerIdAsync(user.CustomerId);
                var data = result.Select(x => _mapper.Map<TransactionViewModel>(x)).ToList();
                return new MethodResult<List<TransactionViewModel>>.Success(data);
            });

        public async Task<bool> CreateTransactionAsync(Transaction transaction)
        {
            return await _transactionRepository.CreateTransactionAsync(transaction);
        }
    }
}
