using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.Language
{
    public class CreateLanguageDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Flag { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDefault { get; set; }
    }
}