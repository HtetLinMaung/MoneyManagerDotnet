using MoneyManager.DTOs;
using MoneyManager.Models;

namespace MoneyManager.Repositories
{
    public interface IFinancialTransactionRepository
    {
        Task<FinancialTransactionsResponse> GetAllAsync(int page, int limit, string fromDate, string toDate, string userId);

        Task<FinancialTransaction?> GetByIdAsync(int id, string userId);

        Task<FinancialTransaction> AddAsync(FinancialTransaction financialTransaction);

        Task UpdateAsync(FinancialTransaction financialTransaction);

        Task DeleteAsync(int id, string userId);
    }
}