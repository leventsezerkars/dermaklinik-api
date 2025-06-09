using DermaKlinik.API.Application.DTOs.Language;

namespace DermaKlinik.API.Application.Services
{
    public interface ILanguageService
    {
        Task<LanguageDto> GetByIdAsync(Guid id);
        Task<IEnumerable<LanguageDto>> GetAllAsync();
        Task<LanguageDto> CreateAsync(CreateLanguageDto createLanguageDto);
        Task<LanguageDto> UpdateAsync(Guid id, UpdateLanguageDto updateLanguageDto);
        Task DeleteAsync(Guid id);
        Task<LanguageDto> GetByCodeAsync(string code);
        Task<bool> IsCodeUniqueAsync(string code);
        Task<bool> IsDefaultLanguageExistsAsync();
        Task<LanguageDto> GetDefaultLanguageAsync();
        Task SetDefaultLanguageAsync(string code);
    }
}