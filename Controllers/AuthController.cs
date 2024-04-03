using Microsoft.AspNetCore.Mvc;
using MoneyManager.DTOs;
using MoneyManager.Services;

namespace MoneyManager.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;

        public AuthController(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(RegisterDto registerDto)
        {
            var response = await _applicationUserService.register(registerDto);
            if (response.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginDto loginDto)
        {
            var response = await _applicationUserService.login(loginDto);
            if (response.Code == StatusCodes.Status401Unauthorized)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }
    }
}