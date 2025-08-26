using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryImage.Commands
{
    public class CreateGalleryImageCommand : IRequest<ApiResponse<GalleryImageDto>>
    {
        public CreateGalleryImageDto CreateGalleryImageDto { get; set; }
    }

    public class CreateGalleryImageCommandHandler : IRequestHandler<CreateGalleryImageCommand, ApiResponse<GalleryImageDto>>
    {
        private readonly IGalleryImageService _galleryImageService;

        public CreateGalleryImageCommandHandler(IGalleryImageService galleryImageService)
        {
            _galleryImageService = galleryImageService;
        }

        public async Task<ApiResponse<GalleryImageDto>> Handle(CreateGalleryImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _galleryImageService.CreateAsync(request.CreateGalleryImageDto);
                return ApiResponse<GalleryImageDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<GalleryImageDto>.ErrorResult(ex.Message);
            }
        }
    }
}
