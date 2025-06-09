using AutoMapper;
using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.Mappings
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<Core.Entities.Language, LanguageDto>();
            CreateMap<CreateLanguageDto, Core.Entities.Language>();
            CreateMap<UpdateLanguageDto, Core.Entities.Language>();
        }
    }
} 