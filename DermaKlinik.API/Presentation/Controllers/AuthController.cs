using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Application.Features.Auth.Commands.Login;
using DermaKlinik.API.Application.Features.Auth.Commands.Logout;
using DermaKlinik.API.Application.Features.Auth.Commands.Register;
using DermaKlinik.API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login([FromBody] LoginDto request)
        {
            if (request == null)
                return BadRequest(ApiResponse<LoginResponseDto>.ErrorResult("Geçersiz istek"));

            var command = new LoginCommand { LoginDto = request };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Register([FromBody] CreateUserDto request)
        {
            if (request == null)
                return BadRequest(ApiResponse<UserDto>.ErrorResult("Geçersiz istek"));

            var command = new RegisterCommand { CreateUserDto = request };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<ApiResponse<bool>>> Logout()
        {
            var command = new LogoutCommand();
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}