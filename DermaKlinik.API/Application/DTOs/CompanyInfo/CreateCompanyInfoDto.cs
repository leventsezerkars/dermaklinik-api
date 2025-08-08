using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.CompanyInfo
{
    public class CreateCompanyInfoDto
    {
        [Required(ErrorMessage = "Şirket adı zorunludur")]
        [StringLength(255, ErrorMessage = "Şirket adı en fazla 255 karakter olabilir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adres zorunludur")]
        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur")]
        [StringLength(100, ErrorMessage = "Telefon numarası en fazla 100 karakter olabilir")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [StringLength(255, ErrorMessage = "E-posta adresi en fazla 255 karakter olabilir")]
        public string Email { get; set; }

        [StringLength(5000, ErrorMessage = "Logo URL'si en fazla 5000 karakter olabilir")]
        public string LogoUrl { get; set; }

        [StringLength(255, ErrorMessage = "Facebook URL'si en fazla 255 karakter olabilir")]
        public string Facebook { get; set; }

        [StringLength(255, ErrorMessage = "Twitter URL'si en fazla 255 karakter olabilir")]
        public string Twitter { get; set; }

        [StringLength(255, ErrorMessage = "Instagram URL'si en fazla 255 karakter olabilir")]
        public string Instagram { get; set; }

        [StringLength(255, ErrorMessage = "LinkedIn URL'si en fazla 255 karakter olabilir")]
        public string LinkedIn { get; set; }

        [StringLength(1000, ErrorMessage = "Google Maps URL'si en fazla 1000 karakter olabilir")]
        public string GoogleMapsUrl { get; set; }

        [StringLength(1000, ErrorMessage = "Google Analytics kodu en fazla 1000 karakter olabilir")]
        public string GoogleAnalyticsCode { get; set; }

        [StringLength(255, ErrorMessage = "Meta başlık en fazla 255 karakter olabilir")]
        public string MetaTitle { get; set; }

        [StringLength(1000, ErrorMessage = "Meta açıklama en fazla 1000 karakter olabilir")]
        public string MetaDescription { get; set; }

        [StringLength(255, ErrorMessage = "Meta anahtar kelimeler en fazla 255 karakter olabilir")]
        public string MetaKeywords { get; set; }

        [StringLength(255, ErrorMessage = "Çalışma saatleri en fazla 255 karakter olabilir")]
        public string WorkingHours { get; set; }

        public bool IsActive { get; set; } = true;
    }
}