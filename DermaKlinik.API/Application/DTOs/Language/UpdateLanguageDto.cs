using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.Language
{
    public class UpdateLanguageDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Flag { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }
    }
}