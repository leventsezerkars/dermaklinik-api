using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.BlogCategory.Queries
{
    public class GetBlogCategoryByIdQuery : IRequest<ApiResponse<BlogCategoryDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetBlogCategoryByIdQueryHandler : IRequestHandler<GetBlogCategoryByIdQuery, ApiResponse<BlogCategoryDto>>
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public GetBlogCategoryByIdQueryHandler(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public async Task<ApiResponse<BlogCategoryDto>> Handle(GetBlogCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogCategoryService.GetByIdAsync(request.Id);
                return ApiResponse<BlogCategoryDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<BlogCategoryDto>.ErrorResult(ex.Message);
            }
        }
    }
}
