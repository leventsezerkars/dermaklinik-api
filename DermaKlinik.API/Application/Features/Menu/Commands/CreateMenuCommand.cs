using DermaKlinik.API.Application.DTOs.Menu;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menu.Commands
{
    public class CreateMenuCommand : IRequest<ApiResponse<MenuDto>>
    {
        public CreateMenuDto CreateMenuDto { get; set; }
    }

    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ApiResponse<MenuDto>>
    {
        private readonly IMenuService _menuService;

        public CreateMenuCommandHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<MenuDto>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _menuService.CreateAsync(request.CreateMenuDto);
                return ApiResponse<MenuDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<MenuDto>.ErrorResult(ex.Message);
            }
        }
    }
}