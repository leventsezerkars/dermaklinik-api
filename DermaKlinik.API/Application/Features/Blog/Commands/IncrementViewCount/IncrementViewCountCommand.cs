using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Blog.Commands
{
    public class IncrementViewCountCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class IncrementViewCountCommandHandler : IRequestHandler<IncrementViewCountCommand, ApiResponse<bool>>
    {
        private readonly IBlogService _blogService;

        public IncrementViewCountCommandHandler(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<ApiResponse<bool>> Handle(IncrementViewCountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _blogService.IncrementViewCountAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
        }
    }
}
