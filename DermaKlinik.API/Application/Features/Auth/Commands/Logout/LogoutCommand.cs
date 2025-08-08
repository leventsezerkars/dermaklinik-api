using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest<ApiResponse<bool>>
    {
    }

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResponse<bool>>
    {
        private readonly IAuthService _authService;

        public LogoutCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ApiResponse<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LogoutAsync();
        }
    }
}
