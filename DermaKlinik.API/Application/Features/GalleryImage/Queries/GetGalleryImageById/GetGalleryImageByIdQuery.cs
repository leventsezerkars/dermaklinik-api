using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryImage.Queries
{
    public class GetGalleryImageByIdQuery : IRequest<ApiResponse<GalleryImageDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetGalleryImageByIdQueryHandler : IRequestHandler<GetGalleryImageByIdQuery, ApiResponse<GalleryImageDto>>
    {
        private readonly IGalleryImageService _galleryImageService;

        public GetGalleryImageByIdQueryHandler(IGalleryImageService galleryImageService)
        {
            _galleryImageService = galleryImageService;
        }

        public async Task<ApiResponse<GalleryImageDto>> Handle(GetGalleryImageByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _galleryImageService.GetByIdAsync(request.Id);
                return ApiResponse<GalleryImageDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<GalleryImageDto>.ErrorResult(ex.Message);
            }
        }
    }
}
