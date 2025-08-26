using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.Blog
{
    public class BlogTranslationDto : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid BlogId { get; set; }
        public Guid LanguageId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string SeoTitle { get; set; } = string.Empty;
        public string SeoDescription { get; set; } = string.Empty;
        public string SeoKeywords { get; set; } = string.Empty;
        public string? FeaturedImage { get; set; }
        public string? VideoUrl { get; set; }
        public string? DocumentUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}