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
            try
            {
                var menu = await _menuService.GetMenuByIdAsync(request.Id);
                if (menu == null)
                    return ApiResponse<bool>.ErrorResult($"Menü bulunamadı: ID {request.Id}", 404);

                var hasChildren = await _menuService.HasChildMenusAsync(request.Id);
                if (hasChildren)
                    return ApiResponse<bool>.ErrorResult("Bu menünün alt menüleri var. Önce alt menüleri silmelisiniz.");

                await _menuService.DeleteMenuAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true, "Menü başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult($"Menü silinirken bir hata oluştu: {ex.Message}");
            }
        }
    }
} 