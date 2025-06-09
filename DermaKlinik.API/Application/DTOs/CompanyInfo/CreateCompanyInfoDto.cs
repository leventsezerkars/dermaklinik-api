using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.CompanyInfo
{
    public class CreateCompanyInfoDto
    {
        [Required(ErrorMessage = "Şirket adı zorunludur")]
        [StringLength(255, ErrorMessage = "Şirket adı en fazla 255 karakter olabilir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adres zorunludur")]
        [StringLength(1000, ErrorMessage = "Adres en fazla 1000 karakter olabilir")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur")]
        [StringLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [StringLength(255, ErrorMessage = "E-posta adresi en fazla 255 karakter olabilir")]
        public string Email { get; set; }

        [StringLength(255, ErrorMessage = "Vergi dairesi en fazla 255 karakter olabilir")]
        public string TaxOffice { get; set; }

        [StringLength(255, ErrorMessage = "Vergi numarası en fazla 255 karakter olabilir")]
        public string TaxNumber { get; set; }

        [Url(ErrorMessage = "Geçerli bir Facebook URL'si giriniz")]
        [StringLength(255, ErrorMessage = "Facebook URL'si en fazla 255 karakter olabilir")]
        public string FacebookUrl { get; set; }

        [Url(ErrorMessage = "Geçerli bir Instagram URL'si giriniz")]
        [StringLength(255, ErrorMessage = "Instagram URL'si en fazla 255 karakter olabilir")]
        public string InstagramUrl { get; set; }

        [Url(ErrorMessage = "Geçerli bir Twitter URL'si giriniz")]
        [StringLength(255, ErrorMessage = "Twitter URL'si en fazla 255 karakter olabilir")]
        public string TwitterUrl { get; set; }

        [Url(ErrorMessage = "Geçerli bir Youtube URL'si giriniz")]
        [StringLength(255, ErrorMessage = "Youtube URL'si en fazla 255 karakter olabilir")]
        public string YoutubeUrl { get; set; }

        [Url(ErrorMessage = "Geçerli bir LinkedIn URL'si giriniz")]
        [StringLength(255, ErrorMessage = "LinkedIn URL'si en fazla 255 karakter olabilir")]
        public string LinkedInUrl { get; set; }

        [Url(ErrorMessage = "Geçerli bir Google Maps URL'si giriniz")]
        [StringLength(255, ErrorMessage = "Google Maps URL'si en fazla 255 karakter olabilir")]
        public string GoogleMapsUrl { get; set; }

        [Url(ErrorMessage = "Geçerli bir logo URL'si giriniz")]
        [StringLength(255, ErrorMessage = "Logo URL'si en fazla 255 karakter olabilir")]
        public string LogoUrl { get; set; }

        [Url(ErrorMessage = "Geçerli bir favicon URL'si giriniz")]
        [StringLength(255, ErrorMessage = "Favicon URL'si en fazla 255 karakter olabilir")]
        public string FaviconUrl { get; set; }

        [StringLength(255, ErrorMessage = "Meta başlığı en fazla 255 karakter olabilir")]
        public string MetaTitle { get; set; }

        [StringLength(1000, ErrorMessage = "Meta açıklaması en fazla 1000 karakter olabilir")]
        public string MetaDescription { get; set; }

        [StringLength(255, ErrorMessage = "Meta anahtar kelimeleri en fazla 255 karakter olabilir")]
        public string MetaKeywords { get; set; }

        [StringLength(255, ErrorMessage = "Çalışma saatleri en fazla 255 karakter olabilir")]
        public string WorkingHours { get; set; }

        public bool IsActive { get; set; } = true;
    }
}