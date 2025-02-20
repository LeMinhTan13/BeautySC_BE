using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;

namespace BeautySCProject.Service.Interfaces
{
    public interface ITransactionService
    {
        Task<MethodResult<IEnumerable<TransactionViewModel>>> GetAllTransactionAsync(DateTime? startDate, DateTime? endDate, int pageSize, int pageIndex);
        Task<MethodResult<List<TransactionViewModel>>> GetTransactionByCustomerAsync(string email);
        Task<bool> CreateTransactionAsync(Transaction transaction);
    }
}
