using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Commands
{
    public class CreateCompanyInfoCommand : IRequest<ApiResponse<CompanyInfoDto>>
    {
        public CreateCompanyInfoDto CreateCompanyInfoDto { get; set; }
    }

    public class CreateCompanyInfoCommandHandler : IRequestHandler<CreateCompanyInfoCommand, ApiResponse<CompanyInfoDto>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public CreateCompanyInfoCommandHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<CompanyInfoDto>> Handle(CreateCompanyInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _companyInfoService.CreateAsync(request.CreateCompanyInfoDto);
                return ApiResponse<CompanyInfoDto>.SuccessResult(result, "Şirket bilgisi başarıyla oluşturuldu");
            }
            catch (InvalidOperationException ex)
            {
                return ApiResponse<CompanyInfoDto>.ErrorResult(ex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<CompanyInfoDto>.ErrorResult("Şirket bilgisi oluşturulurken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 