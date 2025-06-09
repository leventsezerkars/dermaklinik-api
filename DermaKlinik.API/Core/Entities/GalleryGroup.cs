using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Core.Entities
{
    public class GalleryGroup : AuditableEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeletable { get; set; }

        public virtual ICollection<GalleryImageGroupMap> ImageMaps { get; set; }
    }
}