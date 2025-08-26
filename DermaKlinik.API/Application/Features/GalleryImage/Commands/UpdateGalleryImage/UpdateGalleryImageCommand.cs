using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryImage.Commands
{
    public class UpdateGalleryImageCommand : IRequest<ApiResponse<GalleryImageDto>>
    {
        public Guid Id { get; set; }
        public UpdateGalleryImageDto UpdateGalleryImageDto { get; set; }
    }

    public class UpdateGalleryImageCommandHandler : IRequestHandler<UpdateGalleryImageCommand, ApiResponse<GalleryImageDto>>
    {
        private readonly IGalleryImageService _galleryImageService;

        public UpdateGalleryImageCommandHandler(IGalleryImageService galleryImageService)
        {
            _galleryImageService = galleryImageService;
        }

        public async Task<ApiResponse<GalleryImageDto>> Handle(UpdateGalleryImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _galleryImageService.UpdateAsync(request.Id, request.UpdateGalleryImageDto);
                return ApiResponse<GalleryImageDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<GalleryImageDto>.ErrorResult(ex.Message);
            }
        }
    }
}
