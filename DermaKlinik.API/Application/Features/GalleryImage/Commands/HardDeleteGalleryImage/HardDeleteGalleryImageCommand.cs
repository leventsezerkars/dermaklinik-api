using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryImage.Commands
{
    public class HardDeleteGalleryImageCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class HardDeleteGalleryImageCommandHandler : IRequestHandler<HardDeleteGalleryImageCommand, ApiResponse<bool>>
    {
        private readonly IGalleryImageService _galleryImageService;

        public HardDeleteGalleryImageCommandHandler(IGalleryImageService galleryImageService)
        {
            _galleryImageService = galleryImageService;
        }

        public async Task<ApiResponse<bool>> Handle(HardDeleteGalleryImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _galleryImageService.HardDeleteAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}
