using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Blog.Commands
{
    public class HardDeleteBlogCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class HardDeleteBlogCommandHandler : IRequestHandler<HardDeleteBlogCommand, ApiResponse<bool>>
    {
        private readonly IBlogService _blogService;

        public HardDeleteBlogCommandHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<ApiResponse<bool>> Handle(HardDeleteBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _blogService.HardDeleteAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true, "Blog kalıcı olarak silindi");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}

