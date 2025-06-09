namespace DermaKlinik.API.Application.DTOs.Menu
{
    public class UpdateMenuTranslationDto
    {
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
    }
}