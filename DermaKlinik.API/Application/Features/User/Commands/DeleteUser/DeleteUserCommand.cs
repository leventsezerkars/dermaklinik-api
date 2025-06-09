using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<bool>>
    {
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.DeleteAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}