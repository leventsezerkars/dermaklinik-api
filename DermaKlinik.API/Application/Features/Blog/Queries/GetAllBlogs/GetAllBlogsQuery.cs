using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Blog.Queries
{
    public class GetAllBlogsQuery : IRequest<ApiResponse<List<BlogDto>>>
    {
        public PagingRequestModel PagingRequest { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? LanguageId { get; set; }
    }

    public class GetAllBlogsQueryHandler : IRequestHandler<GetAllBlogsQuery, ApiResponse<List<BlogDto>>>
    {
        private readonly IBlogService _blogService;

        public GetAllBlogsQueryHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<ApiResponse<List<BlogDto>>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogService.GetAllAsync(request.PagingRequest, request.CategoryId, request.LanguageId);
                return ApiResponse<List<BlogDto>>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<BlogDto>>.ErrorResult(ex.Message);
            }
        }
    }
}
