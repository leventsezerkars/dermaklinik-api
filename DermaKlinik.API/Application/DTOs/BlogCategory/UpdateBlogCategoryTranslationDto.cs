using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.BlogCategory
{
    public class UpdateBlogCategoryTranslationDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid LanguageId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}