using DermaKlinik.API.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.Language
{
    public class UpdateLanguageDto : BaseDto
    {
        [Required(ErrorMessage = "Dil adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Dil adı en fazla 50 karakter olabilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Dil kodu zorunludur.")]
        [StringLength(10, ErrorMessage = "Dil kodu en fazla 10 karakter olabilir.")]
        public string Code { get; set; }

        public bool IsDefault { get; set; }
    }
}