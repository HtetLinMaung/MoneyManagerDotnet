using Microsoft.EntityFrameworkCore;
using MoneyManager.Data;
using MoneyManager.DTOs;
using MoneyManager.Repositories;
using MoneyManager.Utilities;

namespace MoneyManager.Models
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public FinancialTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FinancialTransaction> AddAsync(FinancialTransaction financialTransaction)
        {
            _context.FinancialTransactions.Add(financialTransaction);
            await _context.SaveChangesAsync();
            return financialTransaction;
        }

        public async Task DeleteAsync(int id, string userId)
        {
            // Find the financial transaction by id and userId
            var financialTransaction = await _context.FinancialTransactions
                                                      .FirstOrDefaultAsync(ft => ft.Id == id && ft.UserId == userId);

            // If a matching financial transaction is found, remove it
            if (financialTransaction != null)
            {
                _context.FinancialTransactions.Remove(financialTransaction);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<FinancialTransactionsResponse> GetAllAsync(int page, int limit, string fromDate, string toDate, string userId)
        {
            // Parse the fromDate and toDate strings to DateTime objects
            DateTime.TryParse(fromDate, out DateTime from);
            DateTime.TryParse(toDate, out DateTime to);

            // Ensure the toDate includes transactions from the entire day
            to = to.Date.AddDays(1).AddTicks(-1);

            // Apply the date range filter before pagination
            var query = _context.FinancialTransactions
                                .Where(ft => ft.Date >= from && ft.Date <= to && ft.UserId == userId);

            // Calculate total income and expense
            var totalIncome = await query.Where(ft => ft.Type == TransactionType.Income).SumAsync(ft => ft.Amount);
            var totalExpense = await query.Where(ft => ft.Type == TransactionType.Expense).SumAsync(ft => ft.Amount);

            // Get the total count for the filtered data
            var totalCount = await query.CountAsync();

            // Apply pagination to the filtered data
            var items = await query.Skip((page - 1) * limit)
                                   .Take(limit)
                                   .ToListAsync();

            var pageCounts = (int)Math.Ceiling((double)totalCount / limit);

            return new FinancialTransactionsResponse
            {
                Items = items,
                TotalCount = totalCount,
                PageCounts = pageCounts,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Message = "Financial transactions fetehed successfully."
            };
        }

        public Task<FinancialTransaction?> GetByIdAsync(int id, string userId)
        {
            return _context.FinancialTransactions.FirstOrDefaultAsync(ft => ft.Id == id && ft.UserId == userId);
        }

        public async Task UpdateAsync(FinancialTransaction financialTransaction)
        {
            _context.Entry(financialTransaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}