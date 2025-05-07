using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DermaKlinik.API.Application.Features.Menus.Queries
{
    public class GetActiveMenusQuery : IRequest<ApiResponse<IEnumerable<Menu>>>
    {
    }

    public class GetActiveMenusQueryHandler : IRequestHandler<GetActiveMenusQuery, ApiResponse<IEnumerable<Menu>>>
    {
        private readonly IMenuService _menuService;
        private readonly ILogger<GetActiveMenusQueryHandler> _logger;

        public GetActiveMenusQueryHandler(IMenuService menuService, ILogger<GetActiveMenusQueryHandler> logger)
        {
            _menuService = menuService;
            _logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<Menu>>> Handle(GetActiveMenusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _menuService.GetActiveMenusAsync();
                return ApiResponse<IEnumerable<Menu>>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Aktif menüler getirilirken hata oluştu");
                return ApiResponse<IEnumerable<Menu>>.ErrorResult("Aktif menüler getirilirken bir hata oluştu.");
            }
        }
    }
} 