using DermaKlinik.API.Application.DTOs.CompanyInfo;

namespace DermaKlinik.API.Application.Services
{
    public interface ICompanyInfoService
    {
        Task<CompanyInfoDto> GetByIdAsync(Guid id);
        Task<IEnumerable<CompanyInfoDto>> GetAllAsync();
        Task<CompanyInfoDto> CreateAsync(CreateCompanyInfoDto createCompanyInfoDto);
        Task<CompanyInfoDto> UpdateAsync(Guid id, UpdateCompanyInfoDto updateCompanyInfoDto);
        Task DeleteAsync(Guid id);
    }
}