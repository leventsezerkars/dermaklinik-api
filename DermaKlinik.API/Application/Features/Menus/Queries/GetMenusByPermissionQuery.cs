using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DermaKlinik.API.Application.Features.Menus.Queries
{
    public class GetMenusByPermissionQuery : IRequest<ApiResponse<IEnumerable<Menu>>>
    {
        public string Permission { get; set; }
    }

    public class GetMenusByPermissionQueryHandler : IRequestHandler<GetMenusByPermissionQuery, ApiResponse<IEnumerable<Menu>>>
    {
        private readonly IMenuService _menuService;
        private readonly ILogger<GetMenusByPermissionQueryHandler> _logger;
        private readonly ILogService _logService;

        public GetMenusByPermissionQueryHandler(
            IMenuService menuService, 
            ILogger<GetMenusByPermissionQueryHandler> logger,
            ILogService logService)
        {
            _menuService = menuService;
            _logger = logger;
            _logService = logService;
        }

        public async Task<ApiResponse<IEnumerable<Menu>>> Handle(GetMenusByPermissionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Permission))
                {
                    await _logService.LogWarningAsync(
                        "İzin parametresi boş gönderildi",
                        nameof(GetMenusByPermissionQueryHandler));
                    return ApiResponse<IEnumerable<Menu>>.ErrorResult("İzin parametresi boş olamaz.");
                }

                var result = await _menuService.GetMenusByPermissionAsync(request.Permission);
                
                await _logService.LogInformationAsync(
                    $"İzne göre menüler başarıyla getirildi: {request.Permission}",
                    nameof(GetMenusByPermissionQueryHandler));

                return ApiResponse<IEnumerable<Menu>>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    "İzne göre menüler getirilirken hata oluştu",
                    ex,
                    nameof(GetMenusByPermissionQueryHandler),
                    additionalData: $"Permission: {request.Permission}");

                _logger.LogError(ex, "İzne göre menüler getirilirken hata oluştu: {Permission}", request.Permission);
                return ApiResponse<IEnumerable<Menu>>.ErrorResult("İzne göre menüler getirilirken bir hata oluştu.");
            }
        }
    }
} 