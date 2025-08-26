using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryImage.Commands
{
    public class DeleteGalleryImageCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteGalleryImageCommandHandler : IRequestHandler<DeleteGalleryImageCommand, ApiResponse<bool>>
    {
        private readonly IGalleryImageService _galleryImageService;

        public DeleteGalleryImageCommandHandler(IGalleryImageService galleryImageService)
        {
            _galleryImageService = galleryImageService;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteGalleryImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _galleryImageService.DeleteAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}
