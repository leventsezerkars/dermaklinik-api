using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DermaKlinik.API.Core.Entities
{
    public class BlogTranslation : AuditableEntity
    {
        [Required]
        public Guid BlogId { get; set; }

        [Required]
        public Guid LanguageId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [StringLength(200)]
        public string Slug { get; set; }

        [StringLength(200)]
        public string SeoTitle { get; set; }

        [StringLength(500)]
        public string SeoDescription { get; set; }

        [StringLength(200)]
        public string SeoKeywords { get; set; }

        public string FeaturedImage { get; set; }

        public string VideoUrl { get; set; }

        public string DocumentUrl { get; set; }

        // Navigation Properties
        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
    }
}