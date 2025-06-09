using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.User.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<ApiResponse<IEnumerable<UserDto>>>
    {
        public bool IncludeInactive { get; set; }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ApiResponse<IEnumerable<UserDto>>>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetAllAsync();
                return ApiResponse<IEnumerable<UserDto>>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<UserDto>>.ErrorResult(ex.Message);
            }
        }
    }
}