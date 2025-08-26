using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Blog.Commands
{
    public class DeleteBlogCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, ApiResponse<bool>>
    {
        private readonly IBlogService _blogService;

        public DeleteBlogCommandHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _blogService.DeleteAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}
