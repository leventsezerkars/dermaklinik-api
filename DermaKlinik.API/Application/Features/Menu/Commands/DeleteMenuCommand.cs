using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menu.Commands
{
    public class DeleteMenuCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, ApiResponse<bool>>
    {
        private readonly IMenuService _menuService;

        public DeleteMenuCommandHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _menuService.DeleteAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}