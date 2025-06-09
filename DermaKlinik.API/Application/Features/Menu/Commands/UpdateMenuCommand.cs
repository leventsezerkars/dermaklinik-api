using DermaKlinik.API.Application.DTOs.Menu;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menu.Commands
{
    public class UpdateMenuCommand : IRequest<ApiResponse<MenuDto>>
    {
        public Guid Id { get; set; }
        public UpdateMenuDto UpdateMenuDto { get; set; }
    }

    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, ApiResponse<MenuDto>>
    {
        private readonly IMenuService _menuService;

        public UpdateMenuCommandHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<MenuDto>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _menuService.UpdateAsync(request.Id, request.UpdateMenuDto);
                if (result == null)
                {
                    return ApiResponse<MenuDto>.ErrorResult("Menü bulunamadı");
                }
                return ApiResponse<MenuDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<MenuDto>.ErrorResult(ex.Message);
            }
        }
    }
}