using MoneyManager.Models;

namespace MoneyManager.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(ApplicationUser applicationUser);
    }
}