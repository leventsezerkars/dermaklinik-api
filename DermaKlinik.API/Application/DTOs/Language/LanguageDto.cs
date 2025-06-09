using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.Language
{
    public class LanguageDto : AuditableEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}