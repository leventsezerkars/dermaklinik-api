using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.BlogCategory
{
    public class BlogCategoryTranslationDto : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid BlogCategoryId { get; set; }
        public Guid LanguageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public LanguageDto Language { get; set; }

    }
}