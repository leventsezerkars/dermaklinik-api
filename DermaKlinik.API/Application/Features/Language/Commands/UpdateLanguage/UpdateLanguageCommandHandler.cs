using AutoMapper;
using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Application.Services.Language;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Language.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, ApiResponse<LanguageDto>>
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public UpdateLanguageCommandHandler(ILanguageService languageService, IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<LanguageDto>> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UpdateLanguageDto == null || request.UpdateLanguageDto.Id == Guid.Empty)
                {
                    return ApiResponse<LanguageDto>.ErrorResult("Geçersiz dil bilgisi.");
                }

                var existingLanguage = await _languageService.GetByIdAsync((Guid)request.UpdateLanguageDto.Id);
                if (existingLanguage == null)
                {
                    return ApiResponse<LanguageDto>.ErrorResult("Dil bulunamadı.");
                }

                if (request.UpdateLanguageDto.IsDefault && await _languageService.IsDefaultLanguageExistsAsync())
                {
                    var defaultLanguage = await _languageService.GetDefaultLanguageAsync();
                    if (defaultLanguage.Id != request.UpdateLanguageDto.Id)
                    {
                        return ApiResponse<LanguageDto>.ErrorResult("Zaten varsayılan bir dil mevcut.");
                    }
                }

                var result = await _languageService.UpdateAsync((Guid)request.UpdateLanguageDto.Id, request.UpdateLanguageDto);
                return ApiResponse<LanguageDto>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<LanguageDto>.ErrorResult(ex.Message);
            }
        }
    }
}