using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryImage.Queries
{
    public class GetAllGalleryImagesQuery : IRequest<ApiResponse<List<GalleryImageDto>>>
    {
        public PagingRequestModel PagingRequest { get; set; }
        public Guid? GroupId { get; set; }
    }

    public class GetAllGalleryImagesQueryHandler : IRequestHandler<GetAllGalleryImagesQuery, ApiResponse<List<GalleryImageDto>>>
    {
        private readonly IGalleryImageService _galleryImageService;

        public GetAllGalleryImagesQueryHandler(IGalleryImageService galleryImageService)
        {
            _galleryImageService = galleryImageService;
        }

        public async Task<ApiResponse<List<GalleryImageDto>>> Handle(GetAllGalleryImagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _galleryImageService.GetAllAsync(request.PagingRequest, request.GroupId);
                return ApiResponse<List<GalleryImageDto>>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<GalleryImageDto>>.ErrorResult(ex.Message);
            }
        }
    }
}
