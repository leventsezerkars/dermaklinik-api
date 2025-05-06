using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menus.Queries
{
    public class GetActiveMenusQuery : IRequest<ApiResponse<IEnumerable<Menu>>>
    {
    }

    public class GetActiveMenusQueryHandler : IRequestHandler<GetActiveMenusQuery, ApiResponse<IEnumerable<Menu>>>
    {
        private readonly IMenuService _menuService;

        public GetActiveMenusQueryHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<IEnumerable<Menu>>> Handle(GetActiveMenusQuery request, CancellationToken cancellationToken)
        {
            var result = await _menuService.GetActiveMenusAsync();
            return ApiResponse<IEnumerable<Menu>>.SuccessResult(result);
        }
    }
} 