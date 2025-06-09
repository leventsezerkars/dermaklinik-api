using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DermaKlinik.API.Core.Entities
{
    public class Language : AuditableEntity
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsDefault { get; set; }

        // Navigation Properties
        public virtual ICollection<MenuTranslation> MenuTranslations { get; set; }
        public virtual ICollection<BlogCategoryTranslation> BlogCategoryTranslations { get; set; }
        public virtual ICollection<BlogTranslation> BlogTranslations { get; set; }
    }
}