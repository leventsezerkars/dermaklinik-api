using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.BlogCategory.Queries
{
    public class GetAllBlogCategoriesQuery : IRequest<ApiResponse<List<BlogCategoryDto>>>
    {
        public PagingRequestModel PagingRequest { get; set; }
    }

    public class GetAllBlogCategoriesQueryHandler : IRequestHandler<GetAllBlogCategoriesQuery, ApiResponse<List<BlogCategoryDto>>>
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public GetAllBlogCategoriesQueryHandler(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public async Task<ApiResponse<List<BlogCategoryDto>>> Handle(GetAllBlogCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogCategoryService.GetAllAsync(request.PagingRequest);
                return ApiResponse<List<BlogCategoryDto>>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<BlogCategoryDto>>.ErrorResult(ex.Message);
            }
        }
    }
}
