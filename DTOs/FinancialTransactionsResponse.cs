using MoneyManager.Models;
using MoneyManager.Utilities;

namespace MoneyManager.DTOs
{
    public class FinancialTransactionsResponse : PaginatedList<FinancialTransaction>
    {
        public decimal TotalIncome { get; set; } = 0M;
        public decimal TotalExpense { get; set; } = 0M;

    }
}