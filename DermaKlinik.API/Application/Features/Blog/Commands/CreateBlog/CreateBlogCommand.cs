using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Blog.Commands
{
    public class CreateBlogCommand : IRequest<ApiResponse<BlogDto>>
    {
        public CreateBlogDto CreateBlogDto { get; set; }
    }

    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, ApiResponse<BlogDto>>
    {
        private readonly IBlogService _blogService;

        public CreateBlogCommandHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<ApiResponse<BlogDto>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _blogService.CreateAsync(request.CreateBlogDto);
                
                // Çevirileri oluştur
                foreach (var translation in request.CreateBlogDto.Translations)
                {
                    translation.BlogId = result.Id;
                    await _blogService.CreateTranslationAsync(translation);
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
