using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DermaKlinik.API.Application.DTOs.BlogCategory
{
    public class CreateBlogCategoryDto
    {
        [JsonPropertyName("translations")]
        public List<CreateBlogCategoryTranslationDto> Translations { get; set; } = new List<CreateBlogCategoryTranslationDto>();

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; } = false;
    }
}