using DermaKlinik.API.Application.DTOs.Menu;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menu.Queries
{
    public class GetMenuByIdQuery : IRequest<ApiResponse<MenuDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, ApiResponse<MenuDto>>
    {
        private readonly IMenuService _menuService;

        public GetMenuByIdQueryHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<MenuDto>> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _menuService.GetByIdAsync(request.Id);
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