using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.Blog
{
    public class CreateBlogDto
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public List<CreateBlogTranslationDto> Translations { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        [Required]
        public string Content { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}