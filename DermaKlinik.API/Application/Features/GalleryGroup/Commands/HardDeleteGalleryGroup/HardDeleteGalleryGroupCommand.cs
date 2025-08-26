using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryGroup.Commands
{
    public class HardDeleteGalleryGroupCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class HardDeleteGalleryGroupCommandHandler : IRequestHandler<HardDeleteGalleryGroupCommand, ApiResponse<bool>>
    {
        private readonly IGalleryGroupService _galleryGroupService;

        public HardDeleteGalleryGroupCommandHandler(IGalleryGroupService galleryGroupService)
        {
            _galleryGroupService = galleryGroupService;
        }

        public async Task<ApiResponse<bool>> Handle(HardDeleteGalleryGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _galleryGroupService.HardDeleteAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}
