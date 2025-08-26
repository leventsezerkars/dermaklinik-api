using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryGroup.Commands
{
    public class CreateGalleryGroupCommand : IRequest<ApiResponse<GalleryGroupDto>>
    {
        public CreateGalleryGroupDto CreateGalleryGroupDto { get; set; }
    }

    public class CreateGalleryGroupCommandHandler : IRequestHandler<CreateGalleryGroupCommand, ApiResponse<GalleryGroupDto>>
    {
        private readonly IGalleryGroupService _galleryGroupService;

        public CreateGalleryGroupCommandHandler(IGalleryGroupService galleryGroupService)
        {
            _galleryGroupService = galleryGroupService;
        }

        public async Task<ApiResponse<GalleryGroupDto>> Handle(CreateGalleryGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _galleryGroupService.CreateAsync(request.CreateGalleryGroupDto);
                return ApiResponse<GalleryGroupDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<GalleryGroupDto>.ErrorResult(ex.Message);
            }
        }
    }
}
