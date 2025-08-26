using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryGroup.Queries
{
    public class GetAllGalleryGroupsQuery : IRequest<ApiResponse<List<GalleryGroupDto>>>
    {
        public PagingRequestModel PagingRequest { get; set; }
    }

    public class GetAllGalleryGroupsQueryHandler : IRequestHandler<GetAllGalleryGroupsQuery, ApiResponse<List<GalleryGroupDto>>>
    {
        private readonly IGalleryGroupService _galleryGroupService;

        public GetAllGalleryGroupsQueryHandler(IGalleryGroupService galleryGroupService)
        {
            _galleryGroupService = galleryGroupService;
        }

        public async Task<ApiResponse<List<GalleryGroupDto>>> Handle(GetAllGalleryGroupsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _galleryGroupService.GetAllAsync(request.PagingRequest);
                return ApiResponse<List<GalleryGroupDto>>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<GalleryGroupDto>>.ErrorResult(ex.Message);
            }
        }
    }
}
