using AutoMapper;
using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Application.Services.Language
{
    public class LanguageService(IUnitOfWork unitOfWork, IMapper mapper, ILanguageRepository languageRepository) : ILanguageService
    {
        public async Task<LanguageDto> GetByIdAsync(Guid id)
        {
            var language = await languageRepository.GetByIdAsync(id);
            return mapper.Map<LanguageDto>(language);
        }

        public async Task<IEnumerable<LanguageDto>> GetAllAsync()
        {
            var languages = languageRepository.GetAll();
            return mapper.Map<IEnumerable<LanguageDto>>(languages);
        }

        public async Task<LanguageDto> CreateAsync(CreateLanguageDto createLanguageDto)
        {
            var language = mapper.Map<Core.Entities.Language>(createLanguageDto);
            await languageRepository.AddAsync(language);
            await unitOfWork.CompleteAsync();
            return mapper.Map<LanguageDto>(language);
        }

        public async Task<LanguageDto> UpdateAsync(Guid id, UpdateLanguageDto updateLanguageDto)
        {
            var language = await languageRepository.GetByIdAsync(id);
            if (language == null)
            {
                return null;
            }

            mapper.Map(updateLanguageDto, language);
            languageRepository.Update(language);
            await unitOfWork.CompleteAsync();
            return mapper.Map<LanguageDto>(language);
        }

        public async Task DeleteAsync(Guid id)
        {
            var language = await languageRepository.GetByIdAsync(id);
            if (language != null)
            {
                languageRepository.SoftDelete(language);
                await unitOfWork.CompleteAsync();
            }
        }

        public async Task<LanguageDto> GetByCodeAsync(string code)
        {
            var language = await languageRepository.Query()
                .FirstOrDefaultAsync(l => l.Code == code);

            if (language == null)
            {
                throw new KeyNotFoundException($"Language with code {code} not found.");
            }

            return mapper.Map<LanguageDto>(language);
        }

        public async Task<bool> IsCodeUniqueAsync(string code)
        {
            return !await languageRepository.Query()
                .AnyAsync(l => l.Code == code);
        }

        public async Task<bool> IsDefaultLanguageExistsAsync()
        {
            return await languageRepository.Query()
                .AnyAsync(l => l.IsDefault);
        }

        public async Task<LanguageDto> GetDefaultLanguageAsync()
        {
            var defaultLanguage = await languageRepository.Query()
                .FirstOrDefaultAsync(l => l.IsDefault);

            if (defaultLanguage == null)
            {
                throw new InvalidOperationException("No default language is set.");
            }

            return mapper.Map<LanguageDto>(defaultLanguage);
        }

        public async Task SetDefaultLanguageAsync(string code)
        {
            var language = await languageRepository.Query()
                .FirstOrDefaultAsync(l => l.Code == code);

            if (language == null)
            {
                throw new KeyNotFoundException($"Language with code {code} not found.");
            }

            var currentDefault = await languageRepository.Query()
                .FirstOrDefaultAsync(l => l.IsDefault);

            if (currentDefault != null)
            {
                currentDefault.IsDefault = false;
                languageRepository.Update(currentDefault);
            }

            language.IsDefault = true;
            languageRepository.Update(language);
            await unitOfWork.CompleteAsync();
        }
    }
}