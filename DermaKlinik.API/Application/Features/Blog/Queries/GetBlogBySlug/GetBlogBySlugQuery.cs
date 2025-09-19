using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Blog.Queries
{
    public class GetBlogBySlugQuery : IRequest<ApiResponse<BlogDto>>
    {
        public string Slug { get; set; }
        public Guid? LanguageId { get; set; }
    }

    public class GetBlogBySlugQueryHandler : IRequestHandler<GetBlogBySlugQuery, ApiResponse<BlogDto>>
    {
        private readonly IBlogService _blogService;

        public GetBlogBySlugQueryHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<ApiResponse<BlogDto>> Handle(GetBlogBySlugQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogService.GetBySlugAsync(request.Slug, request.LanguageId);
                return ApiResponse<BlogDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<BlogDto>.ErrorResult(ex.Message);
            }
        }
    }
}
