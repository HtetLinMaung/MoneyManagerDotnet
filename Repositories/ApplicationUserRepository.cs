using Microsoft.AspNetCore.Identity;
using MoneyManager.Models;

namespace MoneyManager.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> AddAsync(ApplicationUser applicationUser, string password)
        {
            return _userManager.CreateAsync(applicationUser, password);
        }

        public Task<ApplicationUser?> GetByUsernameAsync(string username)
        {
            return _userManager.FindByNameAsync(username);

        }

        public Task<bool> VerifyPasswordAsync(ApplicationUser user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }
    }
}