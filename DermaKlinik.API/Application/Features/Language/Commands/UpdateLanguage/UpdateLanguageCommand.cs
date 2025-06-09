using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Language.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand : IRequest<ApiResponse<LanguageDto>>
    {
        public UpdateLanguageDto UpdateLanguageDto { get; set; }
    }
} 