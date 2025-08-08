using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Queries
{
    public class CheckCompanyInfoExistsQuery : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class CheckCompanyInfoExistsQueryHandler : IRequestHandler<CheckCompanyInfoExistsQuery, ApiResponse<bool>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public CheckCompanyInfoExistsQueryHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<bool>> Handle(CheckCompanyInfoExistsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _companyInfoService.ExistsAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(result, "Varlık kontrolü başarıyla tamamlandı");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult("Varlık kontrolü yapılırken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 