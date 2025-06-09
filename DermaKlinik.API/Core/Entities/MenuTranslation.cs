using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DermaKlinik.API.Core.Entities
{
    public class MenuTranslation : AuditableEntity
    {
        public Guid MenuId { get; set; }
        
        [Required]
        public Guid LanguageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        // Navigation Properties
        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
    }
}