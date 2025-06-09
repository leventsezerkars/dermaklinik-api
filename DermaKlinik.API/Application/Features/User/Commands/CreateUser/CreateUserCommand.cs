using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<ApiResponse<UserDto>>
    {
        public CreateUserDto CreateUserDto { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<UserDto>>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.CreateAsync(request.CreateUserDto);
                return ApiResponse<UserDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.ErrorResult(ex.Message);
            }
        }
    }
}