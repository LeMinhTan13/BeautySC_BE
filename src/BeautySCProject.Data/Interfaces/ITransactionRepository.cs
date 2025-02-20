using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;

namespace BeautySCProject.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionViewModel>> GetAllTransactionAsync(DateTime? startDate, DateTime? endDate, int pageSize, int pageIndex);
        Task<IEnumerable<Transaction>> GetTransactionByCustomerIdAsync(int customerId);
        Task<Transaction> GetTransactionByIdAsync(int transactionId);
        Task<bool> CreateTransactionAsync(Transaction transaction);
    }
}
