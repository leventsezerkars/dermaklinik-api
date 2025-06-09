using DermaKlinik.API.Application.DTOs.Menu;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menu.Commands
{
    public class UpdateMenuCommand : IRequest<ApiResponse<MenuDto>>
    {
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
                if (request.UpdateMenuDto.Id == null)
                    return ApiResponse<MenuDto>.ErrorResult("Id alanı null olamaz");

                var result = await _menuService.UpdateAsync((Guid)request.UpdateMenuDto.Id, request.UpdateMenuDto);

                foreach (var item in request.UpdateMenuDto.Translations)
                {
                    await _menuService.UpdateTranslationAsync((Guid)item.Id, item);
                }

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