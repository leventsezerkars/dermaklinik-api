using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menus.Commands
{
    public class CreateMenuCommand : IRequest<ApiResponse<Menu>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public Guid? ParentId { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsVisible { get; set; } = true;
        public string? RequiredPermission { get; set; }
    }

    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ApiResponse<Menu>>
    {
        private readonly IMenuService _menuService;

        public CreateMenuCommandHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.ParentId.HasValue)
                {
                    var parentMenu = await _menuService.GetMenuByIdAsync(request.ParentId.Value);
                    if (parentMenu == null)
                        return ApiResponse<Menu>.ErrorResult("Belirtilen üst menü bulunamadı.", 404);
                }

                var menu = new Menu
                {
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

                var result = await _menuService.CreateMenuAsync(menu);
                return ApiResponse<Menu>.SuccessResult(result, "Menü başarıyla oluşturuldu.", 201);
            }
            catch (Exception ex)
            {
                return ApiResponse<Menu>.ErrorResult($"Menü oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }
    }
} 