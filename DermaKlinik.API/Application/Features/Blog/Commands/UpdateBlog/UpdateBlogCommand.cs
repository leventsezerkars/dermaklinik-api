using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Blog.Commands
{
    public class UpdateBlogCommand : IRequest<ApiResponse<BlogDto>>
    {
        public UpdateBlogDto UpdateBlogDto { get; set; }
    }

    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, ApiResponse<BlogDto>>
    {
        private readonly IBlogService _blogService;

        public UpdateBlogCommandHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<ApiResponse<BlogDto>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogService.UpdateAsync(request.UpdateBlogDto.Id, request.UpdateBlogDto);
                
                // Çevirileri güncelle
                foreach (var translation in request.UpdateBlogDto.Translations)
                {
                    await _blogService.UpdateTranslationAsync(translation.Id, translation);
                }
                
                return ApiResponse<BlogDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<BlogDto>.ErrorResult(ex.Message);
            }
        }
    }
}
