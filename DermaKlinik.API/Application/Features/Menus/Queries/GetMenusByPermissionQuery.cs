using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menus.Queries
{
    public class GetMenusByPermissionQuery : IRequest<ApiResponse<IEnumerable<Menu>>>
    {
        public string Permission { get; set; }
    }

    public class GetMenusByPermissionQueryHandler : IRequestHandler<GetMenusByPermissionQuery, ApiResponse<IEnumerable<Menu>>>
    {
        private readonly IMenuService _menuService;

        public GetMenusByPermissionQueryHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<IEnumerable<Menu>>> Handle(GetMenusByPermissionQuery request, CancellationToken cancellationToken)
        {
            var result = await _menuService.GetMenusByPermissionAsync(request.Permission);
            return ApiResponse<IEnumerable<Menu>>.SuccessResult(result);
        }
    }
} 