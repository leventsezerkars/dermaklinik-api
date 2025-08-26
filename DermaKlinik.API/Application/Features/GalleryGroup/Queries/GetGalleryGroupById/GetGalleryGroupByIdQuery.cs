using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryGroup.Queries
{
    public class GetGalleryGroupByIdQuery : IRequest<ApiResponse<GalleryGroupDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetGalleryGroupByIdQueryHandler : IRequestHandler<GetGalleryGroupByIdQuery, ApiResponse<GalleryGroupDto>>
    {
        private readonly IGalleryGroupService _galleryGroupService;

        public GetGalleryGroupByIdQueryHandler(IGalleryGroupService galleryGroupService)
        {
            _galleryGroupService = galleryGroupService;
        }

        public async Task<ApiResponse<GalleryGroupDto>> Handle(GetGalleryGroupByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _galleryGroupService.GetByIdAsync(request.Id);
                return ApiResponse<GalleryGroupDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<GalleryGroupDto>.ErrorResult(ex.Message);
            }
        }
    }
}
