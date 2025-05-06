using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menus.Queries
{
    public class GetAllMenusQuery : IRequest<ApiResponse<PaginatedList<Menu>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, ApiResponse<PaginatedList<Menu>>>
    {
        private readonly IMenuService _menuService;

        public GetAllMenusQueryHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<PaginatedList<Menu>>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            var result = await _menuService.GetAllMenusAsync(request.PageNumber, request.PageSize);
            return ApiResponse<PaginatedList<Menu>>.SuccessResult(result);
        }
    }
} 