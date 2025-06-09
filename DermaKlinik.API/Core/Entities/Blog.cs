using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DermaKlinik.API.Core.Entities
{
    public class Blog : AuditableEntity
    {
        [Required]
        public Guid CategoryId { get; set; }

        public int ViewCount { get; set; }

        // Navigation Properties
        [ForeignKey("CategoryId")]
        public virtual BlogCategory Category { get; set; }

        public virtual ICollection<BlogTranslation> Translations { get; set; }
    }
}