using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryImage.Commands
{
    public class AddToGroupCommand : IRequest<ApiResponse<bool>>
    {
        public Guid ImageId { get; set; }
        public Guid GroupId { get; set; }
        public int SortOrder { get; set; }
    }

    public class AddToGroupCommandHandler : IRequestHandler<AddToGroupCommand, ApiResponse<bool>>
    {
        private readonly IGalleryImageService _galleryImageService;

        public AddToGroupCommandHandler(IGalleryImageService galleryImageService)
        {
            _galleryImageService = galleryImageService;
        }

        public async Task<ApiResponse<bool>> Handle(AddToGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _galleryImageService.AddToGroupAsync(request.ImageId, request.GroupId, request.SortOrder);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}
