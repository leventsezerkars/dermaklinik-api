using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menus.Queries
{
    public class GetRootMenusQuery : IRequest<ApiResponse<IEnumerable<Menu>>>
    {
    }

    public class GetRootMenusQueryHandler : IRequestHandler<GetRootMenusQuery, ApiResponse<IEnumerable<Menu>>>
    {
        private readonly IMenuService _menuService;

        public GetRootMenusQueryHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<IEnumerable<Menu>>> Handle(GetRootMenusQuery request, CancellationToken cancellationToken)
        {
            var result = await _menuService.GetRootMenusAsync();
            return ApiResponse<IEnumerable<Menu>>.SuccessResult(result);
        }
    }
} 