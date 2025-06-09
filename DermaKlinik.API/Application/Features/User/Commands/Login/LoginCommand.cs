using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.User.Commands.Login
{
    public class LoginCommand : IRequest<ApiResponse<LoginResponseDto>>
    {
        public LoginDto LoginDto { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginResponseDto>>
    {
        private readonly IUserService _userService;

        public LoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.LoginAsync(request.LoginDto);
                if (result == null)
                {
                    return ApiResponse<LoginResponseDto>.ErrorResult("Kullanıcı adı veya şifre hatalı");
                }
                return ApiResponse<LoginResponseDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<LoginResponseDto>.ErrorResult(ex.Message);
            }
        }
    }
}