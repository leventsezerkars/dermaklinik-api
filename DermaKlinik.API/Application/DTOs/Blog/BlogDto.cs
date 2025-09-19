using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Application.DTOs.BlogCategory;

namespace DermaKlinik.API.Application.DTOs.Blog
{
    public class BlogDto : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<BlogTranslationDto> Translations { get; set; }
        public BlogTranslationDto CurrentTranslation { get; set; }
        public BlogCategoryDto Category { get; set; }
    }
}