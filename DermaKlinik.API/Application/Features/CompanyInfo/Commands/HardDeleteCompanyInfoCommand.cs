using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Commands
{
    public class HardDeleteCompanyInfoCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class HardDeleteCompanyInfoCommandHandler : IRequestHandler<HardDeleteCompanyInfoCommand, ApiResponse<bool>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public HardDeleteCompanyInfoCommandHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<bool>> Handle(HardDeleteCompanyInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _companyInfoService.HardDeleteAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true, "Şirket bilgisi kalıcı olarak silindi");
            }
            catch (KeyNotFoundException ex)
            {
                return ApiResponse<bool>.ErrorResult(ex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult("Şirket bilgisi kalıcı olarak silinirken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 