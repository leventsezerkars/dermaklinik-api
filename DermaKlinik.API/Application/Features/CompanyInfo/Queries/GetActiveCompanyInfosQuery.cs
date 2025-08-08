using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Queries
{
    public class GetActiveCompanyInfosQuery : IRequest<ApiResponse<List<CompanyInfoDto>>>
    {
    }

    public class GetActiveCompanyInfosQueryHandler : IRequestHandler<GetActiveCompanyInfosQuery, ApiResponse<List<CompanyInfoDto>>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public GetActiveCompanyInfosQueryHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<List<CompanyInfoDto>>> Handle(GetActiveCompanyInfosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _companyInfoService.GetActiveAsync();
                return ApiResponse<List<CompanyInfoDto>>.SuccessResult(result, "Aktif şirket bilgileri başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<CompanyInfoDto>>.ErrorResult("Aktif şirket bilgileri getirilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 