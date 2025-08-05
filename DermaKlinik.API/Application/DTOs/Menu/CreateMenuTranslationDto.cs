using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.Menu
{
    public class CreateMenuTranslationDto : BaseDto
    {
        public Guid? MenuId { get; set; }
        public Guid LanguageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
    }
}