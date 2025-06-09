using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<ApiResponse<UserDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetByIdAsync(request.Id);
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