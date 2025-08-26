using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryImage.Commands
{
    public class RemoveFromGroupCommand : IRequest<ApiResponse<bool>>
    {
        public Guid ImageId { get; set; }
        public Guid GroupId { get; set; }
    }

    public class RemoveFromGroupCommandHandler : IRequestHandler<RemoveFromGroupCommand, ApiResponse<bool>>
    {
        private readonly IGalleryImageService _galleryImageService;

        public RemoveFromGroupCommandHandler(IGalleryImageService galleryImageService)
        {
            _galleryImageService = galleryImageService;
        }

        public async Task<ApiResponse<bool>> Handle(RemoveFromGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _galleryImageService.RemoveFromGroupAsync(request.ImageId, request.GroupId);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}
