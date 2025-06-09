using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.User.Queries.GetUserByUsername
{
    public class GetUserByUsernameQuery : IRequest<ApiResponse<UserDto>>
    {
        public string Username { get; set; }
    }

    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, ApiResponse<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUserByUsernameQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<UserDto>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetByUsernameAsync(request.Username);
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