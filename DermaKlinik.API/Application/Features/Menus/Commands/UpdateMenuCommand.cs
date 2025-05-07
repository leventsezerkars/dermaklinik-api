using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menus.Commands
{
    public class UpdateMenuCommand : IRequest<ApiResponse<Menu>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public Guid? ParentId { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public string? RequiredPermission { get; set; }
    }

    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, ApiResponse<Menu>>
    {
        private readonly IMenuService _menuService;

        public UpdateMenuCommandHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<Menu>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingMenu = await _menuService.GetMenuByIdAsync(request.Id);
                if (existingMenu == null)
                    return ApiResponse<Menu>.ErrorResult($"Menü bulunamadı: ID {request.Id}", 404);

                if (request.ParentId.HasValue)
                {
                    var parentMenu = await _menuService.GetMenuByIdAsync(request.ParentId.Value);
                    if (parentMenu == null)
                        return ApiResponse<Menu>.ErrorResult("Belirtilen üst menü bulunamadı.", 404);

                    if (request.ParentId.Value == request.Id)
                        return ApiResponse<Menu>.ErrorResult("Bir menü kendisini üst menü olarak seçemez.");
                }

                var menu = new Menu
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    Icon = request.Icon,
                    Url = request.Url,
                    ParentId = request.ParentId,
                    Order = request.Order,
                    IsActive = request.IsActive,
                    IsVisible = request.IsVisible,
                    RequiredPermission = request.RequiredPermission
                };

                await _menuService.UpdateMenuAsync(menu);
                return ApiResponse<Menu>.SuccessResult(menu, "Menü başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return ApiResponse<Menu>.ErrorResult($"Menü güncellenirken bir hata oluştu: {ex.Message}");
            }
        }
    }
} 