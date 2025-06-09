namespace DermaKlinik.API.Application.DTOs.BlogCategory
{
    public class UpdateBlogCategoryTranslationDto
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
    }
}