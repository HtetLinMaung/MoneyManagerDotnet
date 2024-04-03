using MoneyManager.DTOs;
using MoneyManager.Models;
using MoneyManager.Utilities;

namespace MoneyManager.Services
{
    public interface IFinancialTransactionService
    {
        Task<FinancialTransactionsResponse> GetAllTransactionsAsync(int page, int limit, string fromDate, string toDate, string userId);
        Task<FinancialTransaction?> GetTransactionByIdAsync(int id, string userId);
        Task<FinancialTransaction> AddTransactionAsync(CreateFinancialTransactionDto createFinancialTransactionDto);
        Task UpdateTransactionAsync(UpdateFinancialTransactionDto updateFinancialTransactionDto);
        Task DeleteTransactionAsync(int id, string userId);
    }
}