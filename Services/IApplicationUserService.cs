using MoneyManager.DTOs;
using MoneyManager.Utilities;

namespace MoneyManager.Services
{
    public interface IApplicationUserService
    {
        Task<BaseResponse> register(RegisterDto registerDto);

        Task<BaseResponse> login(LoginDto loginDto);

        
    }
}