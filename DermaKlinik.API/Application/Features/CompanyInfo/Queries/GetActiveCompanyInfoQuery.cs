using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Queries
{
    public class GetActiveCompanyInfoQuery : IRequest<ApiResponse<CompanyInfoDto>>
    {
    }

    public class GetActiveCompanyInfoQueryHandler : IRequestHandler<GetActiveCompanyInfoQuery, ApiResponse<CompanyInfoDto>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public GetActiveCompanyInfoQueryHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<CompanyInfoDto>> Handle(GetActiveCompanyInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _companyInfoService.GetActiveCompanyInfoAsync();
                if (result == null)
                {
                    return ApiResponse<CompanyInfoDto>.ErrorResult("Aktif şirket bilgisi bulunamadı");
                }
                return ApiResponse<CompanyInfoDto>.SuccessResult(result, "Aktif şirket bilgisi başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return ApiResponse<CompanyInfoDto>.ErrorResult("Aktif şirket bilgisi getirilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 