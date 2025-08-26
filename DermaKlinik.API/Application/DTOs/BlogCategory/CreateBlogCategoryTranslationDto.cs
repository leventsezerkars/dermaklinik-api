using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DermaKlinik.API.Application.DTOs.BlogCategory
{
    public class CreateBlogCategoryTranslationDto
    {
        [JsonPropertyName("blogCategoryId")]
        public Guid? BlogCategoryId { get; set; }

        [Required]
        [JsonPropertyName("languageId")]
        public Guid LanguageId { get; set; }

        [Required]
        [StringLength(255)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; } = false;
    }
}