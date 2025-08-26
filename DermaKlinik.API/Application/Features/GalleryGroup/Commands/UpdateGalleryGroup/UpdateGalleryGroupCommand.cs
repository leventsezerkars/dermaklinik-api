using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryGroup.Commands
{
    public class UpdateGalleryGroupCommand : IRequest<ApiResponse<GalleryGroupDto>>
    {
        public Guid Id { get; set; }
        public UpdateGalleryGroupDto UpdateGalleryGroupDto { get; set; }
    }

    public class UpdateGalleryGroupCommandHandler : IRequestHandler<UpdateGalleryGroupCommand, ApiResponse<GalleryGroupDto>>
    {
        private readonly IGalleryGroupService _galleryGroupService;

        public UpdateGalleryGroupCommandHandler(IGalleryGroupService galleryGroupService)
        {
            _galleryGroupService = galleryGroupService;
        }

        public async Task<ApiResponse<GalleryGroupDto>> Handle(UpdateGalleryGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _galleryGroupService.UpdateAsync(request.Id, request.UpdateGalleryGroupDto);
                return ApiResponse<GalleryGroupDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<GalleryGroupDto>.ErrorResult(ex.Message);
            }
        }
    }
}
