using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.Blog
{
    public class CreateBlogDto
    {
        [Required]
        public Guid CategoryId { get; set; }

        public List<CreateBlogTranslationDto> Translations { get; set; } = new List<CreateBlogTranslationDto>();

        public bool IsActive { get; set; } = true;
    }
}