using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Queries
{
    public class GetCompanyInfoByIdQuery : IRequest<ApiResponse<CompanyInfoDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetCompanyInfoByIdQueryHandler : IRequestHandler<GetCompanyInfoByIdQuery, ApiResponse<CompanyInfoDto>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public GetCompanyInfoByIdQueryHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<CompanyInfoDto>> Handle(GetCompanyInfoByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _companyInfoService.GetByIdAsync(request.Id);
                if (result == null)
                {
                    return ApiResponse<CompanyInfoDto>.ErrorResult("Şirket bilgisi bulunamadı");
                }
                return ApiResponse<CompanyInfoDto>.SuccessResult(result, "Şirket bilgisi başarıyla getirildi");
            }
            catch (KeyNotFoundException ex)
            {
                return ApiResponse<CompanyInfoDto>.ErrorResult(ex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<CompanyInfoDto>.ErrorResult("Şirket bilgisi getirilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 