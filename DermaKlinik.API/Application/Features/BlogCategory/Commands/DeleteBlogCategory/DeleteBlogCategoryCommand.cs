using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.BlogCategory.Commands
{
    public class DeleteBlogCategoryCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteBlogCategoryCommandHandler : IRequestHandler<DeleteBlogCategoryCommand, ApiResponse<bool>>
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public DeleteBlogCategoryCommandHandler(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _blogCategoryService.DeleteAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}
