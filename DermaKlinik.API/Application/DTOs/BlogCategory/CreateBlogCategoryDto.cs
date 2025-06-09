using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Application.DTOs.BlogCategory
{
    public class CreateBlogCategoryDto
    {
        [Required(ErrorMessage = "Kategori adı zorunludur")]
        [StringLength(255, ErrorMessage = "Kategori adı en fazla 255 karakter olabilir")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Slug zorunludur")]
        [StringLength(255, ErrorMessage = "Slug en fazla 255 karakter olabilir")]
        [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Slug sadece küçük harf, rakam ve tire içerebilir")]
        public string Slug { get; set; }

        [StringLength(255, ErrorMessage = "Meta başlığı en fazla 255 karakter olabilir")]
        public string MetaTitle { get; set; }

        [StringLength(1000, ErrorMessage = "Meta açıklaması en fazla 1000 karakter olabilir")]
        public string MetaDescription { get; set; }

        [StringLength(255, ErrorMessage = "Meta anahtar kelimeleri en fazla 255 karakter olabilir")]
        public string MetaKeywords { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "En az bir dil çevirisi zorunludur")]
        public List<CreateBlogCategoryTranslationDto> Translations { get; set; }
    }

}