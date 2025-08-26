using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.BlogCategory.Commands
{
    public class CreateBlogCategoryCommand : IRequest<ApiResponse<BlogCategoryDto>>
    {
        public CreateBlogCategoryDto CreateBlogCategoryDto { get; set; }
    }

    public class CreateBlogCategoryCommandHandler : IRequestHandler<CreateBlogCategoryCommand, ApiResponse<BlogCategoryDto>>
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public CreateBlogCategoryCommandHandler(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public async Task<ApiResponse<BlogCategoryDto>> Handle(CreateBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogCategoryService.CreateAsync(request.CreateBlogCategoryDto);
                
                // Çevirileri oluştur
                foreach (var translation in request.CreateBlogCategoryDto.Translations)
                {
                    translation.BlogCategoryId = result.Id;
                    await _blogCategoryService.CreateTranslationAsync(translation);
                }
                
                return ApiResponse<BlogCategoryDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<BlogCategoryDto>.ErrorResult(ex.Message);
            }
        }
    }
}
