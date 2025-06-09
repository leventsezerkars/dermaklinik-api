using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Language.Queries.GetAllLanguages
{
    public class GetAllLanguagesQuery : IRequest<ApiResponse<List<LanguageDto>>>
    {
    }
} 