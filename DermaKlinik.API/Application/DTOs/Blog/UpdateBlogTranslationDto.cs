using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.Blog
{
    public class UpdateBlogTranslationDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        public string LanguageCode { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        [StringLength(255)]
        public string SeoTitle { get; set; }

        [StringLength(500)]
        public string SeoDescription { get; set; }

        [StringLength(500)]
        public string SeoKeywords { get; set; }

        public bool IsActive { get; set; }
    }
}