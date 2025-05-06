using Microsoft.AspNetCore.Mvc;
using DermaKlinik.API.Core.Models.Auth;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<User>>> Register(RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("logout")]
        public ActionResult<ApiResponse<bool>> Logout()
        {
            var response = _authService.Logout();
            return Ok(response);
        }
    }
} 