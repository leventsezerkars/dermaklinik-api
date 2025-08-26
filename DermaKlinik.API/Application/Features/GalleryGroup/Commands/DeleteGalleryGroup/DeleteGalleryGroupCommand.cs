using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryGroup.Commands
{
    public class DeleteGalleryGroupCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteGalleryGroupCommandHandler : IRequestHandler<DeleteGalleryGroupCommand, ApiResponse<bool>>
    {
        private readonly IGalleryGroupService _galleryGroupService;

        public DeleteGalleryGroupCommandHandler(IGalleryGroupService galleryGroupService)
        {
            _galleryGroupService = galleryGroupService;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteGalleryGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _galleryGroupService.DeleteAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}
