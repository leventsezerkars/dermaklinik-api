using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.Blog
{
    public class CreateBlogTranslationDto
    {
        public Guid? BlogId { get; set; }

        [Required]
        public Guid LanguageId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Slug { get; set; } = string.Empty;

        [StringLength(200)]
        public string? SeoTitle { get; set; }

        [StringLength(500)]
        public string? SeoDescription { get; set; }

        [StringLength(200)]
        public string? SeoKeywords { get; set; }

        public string? FeaturedImage { get; set; }

        public string? VideoUrl { get; set; }

        public string? DocumentUrl { get; set; }
    }
}