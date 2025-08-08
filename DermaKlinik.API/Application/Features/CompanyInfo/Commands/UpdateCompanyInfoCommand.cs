using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.CompanyInfo.Commands
{
    public class UpdateCompanyInfoCommand : IRequest<ApiResponse<CompanyInfoDto>>
    {
        public Guid Id { get; set; }
        public UpdateCompanyInfoDto UpdateCompanyInfoDto { get; set; }
    }

    public class UpdateCompanyInfoCommandHandler : IRequestHandler<UpdateCompanyInfoCommand, ApiResponse<CompanyInfoDto>>
    {
        private readonly ICompanyInfoService _companyInfoService;

        public UpdateCompanyInfoCommandHandler(ICompanyInfoService companyInfoService)
        {
            _companyInfoService = companyInfoService;
        }

        public async Task<ApiResponse<CompanyInfoDto>> Handle(UpdateCompanyInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.UpdateCompanyInfoDto.Id = request.Id;
                var result = await _companyInfoService.UpdateAsync(request.Id, request.UpdateCompanyInfoDto);
                return ApiResponse<CompanyInfoDto>.SuccessResult(result, "Şirket bilgisi başarıyla güncellendi");
            }
            catch (KeyNotFoundException ex)
            {
                return ApiResponse<CompanyInfoDto>.ErrorResult(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ApiResponse<CompanyInfoDto>.ErrorResult(ex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<CompanyInfoDto>.ErrorResult("Şirket bilgisi güncellenirken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 