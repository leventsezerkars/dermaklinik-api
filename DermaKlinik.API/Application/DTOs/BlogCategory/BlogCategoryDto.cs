using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.BlogCategory
{
    public class BlogCategoryDto : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<BlogDto> Blogs { get; set; }
        public List<BlogCategoryTranslationDto> Translations { get; set; }
    }
}