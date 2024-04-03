namespace MoneyManager.Models
{
    public class FinancialTransaction
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public decimal Amount { get; set; } = 0.0M;
        public string Description { get; set; } = "";
        public string Icon { get; set; } = "";
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; } // Expense or Income

        // Navigation properties
        public virtual ApplicationUser User { get; set; }
    }

    public enum TransactionType
    {
        Expense,
        Income
    }
}