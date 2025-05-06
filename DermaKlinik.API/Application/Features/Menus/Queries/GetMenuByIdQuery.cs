using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using MediatR;

namespace DermaKlinik.API.Application.Features.Menus.Queries
{
    public class GetMenuByIdQuery : IRequest<ApiResponse<Menu>>
    {
        public Guid Id { get; set; }
    }

    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, ApiResponse<Menu>>
    {
        private readonly IMenuService _menuService;

        public GetMenuByIdQueryHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<ApiResponse<Menu>> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _menuService.GetMenuByIdAsync(request.Id);
            if (result == null)
                return ApiResponse<Menu>.ErrorResult("Menü bulunamadı.");

            return ApiResponse<Menu>.SuccessResult(result);
        }
    }
} 