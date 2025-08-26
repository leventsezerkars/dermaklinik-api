using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.BlogCategory
{
    public class BlogCategoryDto : AuditableEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<BlogDto> Blogs { get; set; }
        public List<BlogCategoryTranslationDto> Translations { get; set; }
    }
}