using DermaKlinik.API.Application.DTOs.Menu;
using DermaKlinik.API.Application.Models.FilterModels;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menu.Queries
{
    public class GetAllMenusQuery : IRequest<ApiResponse<IEnumerable<MenuDto>>>
    {
        public PagingRequestModel Request { get; set; } = new PagingRequestModel();
        public MenuFilter Filters { get; set; } = new MenuFilter();
    }

    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, ApiResponse<IEnumerable<MenuDto>>>
    {
        private readonly IMenuService _menuService;

        public GetAllMenusQueryHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<IEnumerable<MenuDto>>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _menuService.GetAllAsync(request.Request, request.Filters);
                return ApiResponse<IEnumerable<MenuDto>>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<MenuDto>>.ErrorResult(ex.Message);
            }
        }
    }
}