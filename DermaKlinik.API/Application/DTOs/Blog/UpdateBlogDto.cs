using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.Blog
{
    public class UpdateBlogDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public List<UpdateBlogTranslationDto> Translations { get; set; } = new List<UpdateBlogTranslationDto>();

        public bool IsActive { get; set; } = true;
    }
}