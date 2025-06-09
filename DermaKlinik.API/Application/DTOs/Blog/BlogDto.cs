using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.Blog
{
    public class BlogDto : AuditableEntity
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public List<BlogTranslationDto> Translations { get; set; }
    }
}