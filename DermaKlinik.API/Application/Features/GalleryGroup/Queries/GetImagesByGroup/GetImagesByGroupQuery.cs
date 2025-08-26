using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.GalleryGroup.Queries
{
    public class GetImagesByGroupQuery : IRequest<ApiResponse<List<GalleryImageDto>>>
    {
        public Guid GroupId { get; set; }
        public PagingRequestModel PagingRequest { get; set; }
    }

    public class GetImagesByGroupQueryHandler : IRequestHandler<GetImagesByGroupQuery, ApiResponse<List<GalleryImageDto>>>
    {
        private readonly IGalleryGroupService _galleryGroupService;

        public GetImagesByGroupQueryHandler(IGalleryGroupService galleryGroupService)
        {
            _galleryGroupService = galleryGroupService;
        }

        public async Task<ApiResponse<List<GalleryImageDto>>> Handle(GetImagesByGroupQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _galleryGroupService.GetImagesByGroupAsync(request.GroupId, request.PagingRequest);
                return ApiResponse<List<GalleryImageDto>>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<GalleryImageDto>>.ErrorResult(ex.Message);
            }
        }
    }
}
