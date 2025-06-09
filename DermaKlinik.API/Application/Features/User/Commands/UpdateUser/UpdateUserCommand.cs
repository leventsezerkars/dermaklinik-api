using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<ApiResponse<UserDto>>
    {
        public Guid Id { get; set; }
        public UpdateUserDto UpdateUserDto { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<UserDto>>
    {
        private readonly IUserService _userService;

        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.UpdateAsync(request.Id, request.UpdateUserDto);
                if (result == null)
                {
                    return ApiResponse<UserDto>.ErrorResult("Kullanıcı bulunamadı");
                }
                return ApiResponse<UserDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.ErrorResult(ex.Message);
            }
        }
    }
}