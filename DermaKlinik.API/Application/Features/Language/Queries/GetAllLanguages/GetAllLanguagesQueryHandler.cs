using AutoMapper;
using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Application.Services.Language;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Language.Queries.GetAllLanguages
{
    public class GetAllLanguagesQueryHandler : IRequestHandler<GetAllLanguagesQuery, ApiResponse<List<LanguageDto>>>
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public GetAllLanguagesQueryHandler(ILanguageService languageService, IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<LanguageDto>>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var languages = await _languageService.GetAllAsync();
                return ApiResponse<List<LanguageDto>>.SuccessResult(languages);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<LanguageDto>>.ErrorResult(ex.Message);
            }
        }
    }
} 