using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Queries
{
    public class CheckCompanyInfoNameUniqueQuery : IRequest<ApiResponse<bool>>
    {
        public string Name { get; set; }
        public Guid? ExcludeId { get; set; }
    }

    public class CheckCompanyInfoNameUniqueQueryHandler : IRequestHandler<CheckCompanyInfoNameUniqueQuery, ApiResponse<bool>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public CheckCompanyInfoNameUniqueQueryHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<bool>> Handle(CheckCompanyInfoNameUniqueQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _companyInfoService.IsNameUniqueAsync(request.Name, request.ExcludeId);
                return ApiResponse<bool>.SuccessResult(result, "İsim benzersizlik kontrolü başarıyla tamamlandı");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult("İsim benzersizlik kontrolü yapılırken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 