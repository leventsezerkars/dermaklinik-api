using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Language.Commands.CreateLanguage
{
    public class CreateLanguageCommand : IRequest<ApiResponse<LanguageDto>>
    {
        public CreateLanguageDto CreateLanguageDto { get; set; }
    }
} 