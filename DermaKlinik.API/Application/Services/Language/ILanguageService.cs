using DermaKlinik.API.Application.DTOs.Language;

namespace DermaKlinik.API.Application.Services.Language
{
    public interface ILanguageService
    {
        Task<List<LanguageDto>> GetAllAsync();
        Task<LanguageDto> GetByIdAsync(Guid id);
        Task<LanguageDto> GetByCodeAsync(string code);
        Task<LanguageDto> CreateAsync(CreateLanguageDto createLanguageDto);
        Task<LanguageDto> UpdateAsync(Guid id, UpdateLanguageDto updateLanguageDto);
        Task DeleteAsync(Guid id);
        Task HardDeleteAsync(Guid id);
        Task<bool> IsCodeUniqueAsync(string code);
        Task<bool> IsDefaultLanguageExistsAsync();
        Task<LanguageDto> GetDefaultLanguageAsync();
        Task SetDefaultLanguageAsync(string code);
    }
}