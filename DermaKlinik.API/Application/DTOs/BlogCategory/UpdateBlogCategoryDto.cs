using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.BlogCategory
{
    public class UpdateBlogCategoryDto
    {
        [Required(ErrorMessage = "ID zorunludur")]
        public Guid Id { get; set; }

        public List<UpdateBlogCategoryTranslationDto> Translations { get; set; } = new List<UpdateBlogCategoryTranslationDto>();

        public bool IsActive { get; set; } = true;
    }
}