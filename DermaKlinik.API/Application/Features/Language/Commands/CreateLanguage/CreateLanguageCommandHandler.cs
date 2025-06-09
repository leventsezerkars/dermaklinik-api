using AutoMapper;
using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Application.Services.Language;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Language.Commands.CreateLanguage
{
    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, ApiResponse<LanguageDto>>
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public CreateLanguageCommandHandler(ILanguageService languageService, IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<LanguageDto>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _languageService.IsCodeUniqueAsync(request.CreateLanguageDto.Code))
                {
                    return ApiResponse<LanguageDto>.ErrorResult("Bu dil kodu zaten kullanılıyor.");
                }

                if (request.CreateLanguageDto.IsDefault && await _languageService.IsDefaultLanguageExistsAsync())
                {
                    return ApiResponse<LanguageDto>.ErrorResult("Zaten varsayılan bir dil mevcut.");
                }

                var result = await _languageService.CreateAsync(request.CreateLanguageDto);
                return ApiResponse<LanguageDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<LanguageDto>.ErrorResult(ex.Message);
            }
        }
    }
} 