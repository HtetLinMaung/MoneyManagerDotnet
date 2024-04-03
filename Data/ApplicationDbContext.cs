using Microsoft.EntityFrameworkCore;
using MoneyManager.Models;

namespace MoneyManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    }
}