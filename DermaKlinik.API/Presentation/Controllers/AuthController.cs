using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
            if (request == null)
                return BadRequest(ApiResponse<string>.ErrorResult("Geçersiz istek"));

            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<User>>> Register(RegisterRequest request)
        {
            if (request == null)
                return BadRequest(ApiResponse<User>.ErrorResult("Geçersiz istek"));

            var response = await _authService.RegisterAsync(request);
            return Ok(response);
        }

        [HttpPost("logout")]
        public ActionResult<ApiResponse<bool>> Logout()
        {
            var response = _authService.Logout();
            return Ok(response);
        }
    }
}