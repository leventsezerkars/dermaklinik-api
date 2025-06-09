using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.User.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<ApiResponse<UserDto>>
    {
        public string Email { get; set; }
    }

    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, ApiResponse<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUserByEmailQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<UserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetByEmailAsync(request.Email);
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