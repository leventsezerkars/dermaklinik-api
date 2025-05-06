using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Interfaces;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menus.Commands
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
            await _menuService.DeleteMenuAsync(request.Id);
            return ApiResponse<bool>.SuccessResult(true, "Menü başarıyla silindi.");
        }
    }
} 