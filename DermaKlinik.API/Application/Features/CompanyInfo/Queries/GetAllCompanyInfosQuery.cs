using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Queries
{
    public class GetAllCompanyInfosQuery : IRequest<ApiResponse<List<CompanyInfoDto>>>
    {
    }

    public class GetAllCompanyInfosQueryHandler : IRequestHandler<GetAllCompanyInfosQuery, ApiResponse<List<CompanyInfoDto>>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public GetAllCompanyInfosQueryHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<List<CompanyInfoDto>>> Handle(GetAllCompanyInfosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _companyInfoService.GetAllAsync();
                return ApiResponse<List<CompanyInfoDto>>.SuccessResult(result, "Şirket bilgileri başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<CompanyInfoDto>>.ErrorResult("Şirket bilgileri getirilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 