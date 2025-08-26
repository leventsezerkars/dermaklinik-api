using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Blog.Queries
{
    public class GetBlogByIdQuery : IRequest<ApiResponse<BlogDto>>
    {
        public Guid Id { get; set; }
        public Guid? LanguageId { get; set; }
    }

    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, ApiResponse<BlogDto>>
    {
        private readonly IBlogService _blogService;

        public GetBlogByIdQueryHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<ApiResponse<BlogDto>> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogService.GetByIdAsync(request.Id, request.LanguageId);
                return ApiResponse<BlogDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<BlogDto>.ErrorResult(ex.Message);
            }
        }
    }
}
