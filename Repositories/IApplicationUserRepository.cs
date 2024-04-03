using Microsoft.AspNetCore.Identity;
using MoneyManager.Models;

namespace MoneyManager.Repositories
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser?> GetByUsernameAsync(string username);

        Task<IdentityResult> AddAsync(ApplicationUser applicationUser, string password);

        Task<bool> VerifyPasswordAsync(ApplicationUser user, string password);
    }
}