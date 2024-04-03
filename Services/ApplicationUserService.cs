using MoneyManager.DTOs;
using MoneyManager.Models;
using MoneyManager.Repositories;
using MoneyManager.Utilities;

namespace MoneyManager.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IJwtService _jwtService;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepository, IJwtService jwtService)
        {
            _applicationUserRepository = applicationUserRepository;
            _jwtService = jwtService;
        }
        public async Task<BaseResponse> login(LoginDto loginDto)
        {
            var applicationUser = await _applicationUserRepository.GetByUsernameAsync(loginDto.Username);
            if (applicationUser == null)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Incorrect username!"
                };
            }
            var isPasswordValid = await _applicationUserRepository.VerifyPasswordAsync(applicationUser, loginDto.Password);
            if (!isPasswordValid)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Incorrect password!"
                };
            }
            var token = _jwtService.GenerateJwtToken(applicationUser);
            return new DataResponse<dynamic>()
            {
                Message = "Login successful.",
                Data = new
                {
                    Token = token,
                    Fullname = applicationUser.FirstName + " " + applicationUser.LastName
                }
            };
        }

        public async Task<BaseResponse> register(RegisterDto registerDto)
        {
            var user = await _applicationUserRepository.GetByUsernameAsync(registerDto.Username);
            if (user != null)
            {
                return new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "User already existed!"
                };
            }

            await _applicationUserRepository.AddAsync(new ApplicationUser
            {
                UserName = registerDto.Username,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            }, registerDto.Password);

            return new BaseResponse
            {
                Message = "User registered successfully."
            };
        }
    }
}