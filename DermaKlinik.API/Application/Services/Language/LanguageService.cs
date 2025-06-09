using AutoMapper;
using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Application.Services.Language
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILanguageRepository languageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _languageRepository = languageRepository;
        }

        public async Task<List<LanguageDto>> GetAllAsync()
        {
            var languages = await _languageRepository.Query()
                .Where(l => l.IsActive && !l.IsDeleted)
                .ToListAsync();

            return _mapper.Map<List<LanguageDto>>(languages);
        }

        public async Task<LanguageDto> GetByIdAsync(Guid id)
        {
            var language = await _languageRepository.GetByIdAsync(id);
            return _mapper.Map<LanguageDto>(language);
        }

        public async Task<LanguageDto> GetByCodeAsync(string code)
        {
            var language = await _languageRepository.Query()
                .FirstOrDefaultAsync(l => l.Code == code && l.IsActive && !l.IsDeleted);

            if (language == null)
            {
                throw new KeyNotFoundException($"Language with code {code} not found.");
            }

            return _mapper.Map<LanguageDto>(language);
        }

        public async Task<LanguageDto> CreateAsync(CreateLanguageDto createLanguageDto)
        {
            var language = _mapper.Map<Core.Entities.Language>(createLanguageDto);
            await _languageRepository.AddAsync(language);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<LanguageDto>(language);
        }

        public async Task<LanguageDto> UpdateAsync(Guid id, UpdateLanguageDto updateLanguageDto)
        {
            var language = await _languageRepository.GetByIdAsync(id);
            if (language == null)
            {
                return null;
            }

            _mapper.Map(updateLanguageDto, language);
            _languageRepository.Update(language);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<LanguageDto>(language);
        }

        public async Task DeleteAsync(Guid id)
        {
            var language = await _languageRepository.GetByIdAsync(id);
            if (language != null)
            {
                _languageRepository.SoftDelete(language);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task HardDeleteAsync(Guid id)
        {
            var language = await _languageRepository.GetByIdAsync(id);
            if (language != null)
            {
                _languageRepository.HardDelete(language);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> IsCodeUniqueAsync(string code)
        {
            return !await _languageRepository.Query()
                .AnyAsync(l => l.Code == code);
        }

        public async Task<bool> IsDefaultLanguageExistsAsync()
        {
            return await _languageRepository.Query()
                .AnyAsync(l => l.IsDefault);
        }

        public async Task<LanguageDto> GetDefaultLanguageAsync()
        {
            var defaultLanguage = await _languageRepository.Query()
                .FirstOrDefaultAsync(l => l.IsDefault);

            if (defaultLanguage == null)
            {
                throw new InvalidOperationException("No default language is set.");
            }

            return _mapper.Map<LanguageDto>(defaultLanguage);
        }

        public async Task SetDefaultLanguageAsync(string code)
        {
            var language = await _languageRepository.Query()
                .FirstOrDefaultAsync(l => l.Code == code);

            if (language == null)
            {
                throw new KeyNotFoundException($"Language with code {code} not found.");
            }

            var currentDefault = await _languageRepository.Query()
                .FirstOrDefaultAsync(l => l.IsDefault);

            if (currentDefault != null)
            {
                currentDefault.IsDefault = false;
                _languageRepository.Update(currentDefault);
            }

            language.IsDefault = true;
            _languageRepository.Update(language);
            await _unitOfWork.CompleteAsync();
        }
    }
}