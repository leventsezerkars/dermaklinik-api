using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.Email
{
    public class EmailRequestDto
    {
        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [StringLength(255, ErrorMessage = "E-posta en fazla 255 karakter olabilir")]
        public string Email { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Telefon en fazla 50 karakter olabilir")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Konu alanı zorunludur")]
        [StringLength(200, ErrorMessage = "Konu en fazla 200 karakter olabilir")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mesaj alanı zorunludur")]
        [StringLength(5000, ErrorMessage = "Mesaj en fazla 5000 karakter olabilir")]
        public string Message { get; set; } = string.Empty;

        public string? CompanyName { get; set; }
    }
}
