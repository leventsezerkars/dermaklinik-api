using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Commands
{
    public class DeleteCompanyInfoCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCompanyInfoCommandHandler : IRequestHandler<DeleteCompanyInfoCommand, ApiResponse<bool>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public DeleteCompanyInfoCommandHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteCompanyInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _companyInfoService.DeleteAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true, "Şirket bilgisi başarıyla silindi");
            }
            catch (KeyNotFoundException ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult("Şirket bilgisi silinirken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 