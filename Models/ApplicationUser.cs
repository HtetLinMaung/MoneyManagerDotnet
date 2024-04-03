using Microsoft.AspNetCore.Identity;

namespace MoneyManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        // Navigation properties
        // public virtual ICollection<Expense> Expenses { get; set; }

    }
}