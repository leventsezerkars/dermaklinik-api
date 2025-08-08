using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Queries
{
    public class CheckCompanyInfoEmailUniqueQuery : IRequest<ApiResponse<bool>>
    {
        public string Email { get; set; }
        public Guid? ExcludeId { get; set; }
    }

    public class CheckCompanyInfoEmailUniqueQueryHandler : IRequestHandler<CheckCompanyInfoEmailUniqueQuery, ApiResponse<bool>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public CheckCompanyInfoEmailUniqueQueryHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<bool>> Handle(CheckCompanyInfoEmailUniqueQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _companyInfoService.IsEmailUniqueAsync(request.Email, request.ExcludeId);
                return ApiResponse<bool>.SuccessResult(result, "E-posta benzersizlik kontrolü başarıyla tamamlandı");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult("E-posta benzersizlik kontrolü yapılırken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 