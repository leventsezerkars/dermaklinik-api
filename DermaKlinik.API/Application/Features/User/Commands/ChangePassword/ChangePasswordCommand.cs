using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Application.Services;
using MediatR;

namespace DermaKlinik.API.Application.Features.User.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IUserService _userService;

        public ChangePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var changePasswordDto = new ChangePasswordDto
            {
                Id = request.Id,
                CurrentPassword = request.CurrentPassword,
                NewPassword = request.NewPassword
            };

            await _userService.ChangePasswordAsync(changePasswordDto);
            return Unit.Value;
        }
    }
}