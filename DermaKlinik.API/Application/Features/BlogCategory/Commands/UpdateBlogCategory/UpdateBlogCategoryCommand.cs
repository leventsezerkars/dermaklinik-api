using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.BlogCategory.Commands
{
    public class UpdateBlogCategoryCommand : IRequest<ApiResponse<BlogCategoryDto>>
    {
        public UpdateBlogCategoryDto UpdateBlogCategoryDto { get; set; }
    }

    public class UpdateBlogCategoryCommandHandler : IRequestHandler<UpdateBlogCategoryCommand, ApiResponse<BlogCategoryDto>>
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public UpdateBlogCategoryCommandHandler(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public async Task<ApiResponse<BlogCategoryDto>> Handle(UpdateBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogCategoryService.UpdateAsync(request.UpdateBlogCategoryDto.Id, request.UpdateBlogCategoryDto);
                
                // Çevirileri güncelle
                foreach (var translation in request.UpdateBlogCategoryDto.Translations)
                {
                    await _blogCategoryService.UpdateTranslationAsync(translation.Id, translation);
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
