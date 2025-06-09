using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DermaKlinik.API.Core.Entities
{
    public class BlogCategoryTranslation : AuditableEntity
    {
        [Required]
        public Guid BlogCategoryId { get; set; }

        [Required]
        public Guid LanguageId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [ForeignKey("BlogCategoryId")]
        public virtual BlogCategory BlogCategory { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
    }
}