using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryImage.Commands
{
    public class UpdateImageOrderCommand : IRequest<ApiResponse<bool>>
    {
        public Guid ImageId { get; set; }
        public Guid GroupId { get; set; }
        public int NewSortOrder { get; set; }
    }

    public class UpdateImageOrderCommandHandler : IRequestHandler<UpdateImageOrderCommand, ApiResponse<bool>>
    {
        private readonly IGalleryImageService _galleryImageService;

        public UpdateImageOrderCommandHandler(IGalleryImageService galleryImageService)
        {
            _galleryImageService = galleryImageService;
        }

        public async Task<ApiResponse<bool>> Handle(UpdateImageOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _galleryImageService.UpdateImageOrderAsync(request.ImageId, request.GroupId, request.NewSortOrder);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}
