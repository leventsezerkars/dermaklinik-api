using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Core.Entities
{
    public class GalleryImage : AuditableEntity
    {
        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string AltText { get; set; }

        [StringLength(255)]
        public string Caption { get; set; }

        // Navigation Properties
        public virtual ICollection<GalleryImageGroupMap> GroupMaps { get; set; }
    }
}