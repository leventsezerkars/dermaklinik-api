using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<ApiResponse<UserDto>>
    {
        public CreateUserDto CreateUserDto { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<UserDto>>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ApiResponse<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request.CreateUserDto);
        }
    }
}
