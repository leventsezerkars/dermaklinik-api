using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Application.Models.FilterModels;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Queries
{
    public class GetAllCompanyInfosPagedQuery : IRequest<ApiResponse<List<CompanyInfoDto>>>
    {
        public PagingRequestModel Request { get; set; } = new PagingRequestModel();
        public CompanyInfoFilter Filters { get; set; } = new CompanyInfoFilter();
    }

    public class GetAllCompanyInfosPagedQueryHandler : IRequestHandler<GetAllCompanyInfosPagedQuery, ApiResponse<List<CompanyInfoDto>>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public GetAllCompanyInfosPagedQueryHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<List<CompanyInfoDto>>> Handle(GetAllCompanyInfosPagedQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _companyInfoService.GetAllAsync(request.Request, request.Filters);
                return ApiResponse<List<CompanyInfoDto>>.SuccessResult(result, "Şirket bilgileri başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<CompanyInfoDto>>.ErrorResult("Şirket bilgileri getirilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 