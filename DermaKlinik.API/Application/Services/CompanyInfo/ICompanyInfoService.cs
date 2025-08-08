using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Application.Models.FilterModels;
using DermaKlinik.API.Core.Models;

namespace DermaKlinik.API.Application.Services
{
    public interface ICompanyInfoService
    {
        Task<CompanyInfoDto> GetByIdAsync(Guid id);
        Task<List<CompanyInfoDto>> GetAllAsync();
        Task<List<CompanyInfoDto>> GetAllAsync(PagingRequestModel request, CompanyInfoFilter filters);
        Task<List<CompanyInfoDto>> GetActiveAsync();
        Task<CompanyInfoDto?> GetActiveCompanyInfoAsync();
        Task<CompanyInfoDto> CreateAsync(CreateCompanyInfoDto createCompanyInfoDto);
        Task<CompanyInfoDto> UpdateAsync(Guid id, UpdateCompanyInfoDto updateCompanyInfoDto);
        Task DeleteAsync(Guid id);
        Task HardDeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> IsNameUniqueAsync(string name, Guid? excludeId = null);
        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null);
    }
}