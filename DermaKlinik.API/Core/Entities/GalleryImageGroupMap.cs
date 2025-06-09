using System.ComponentModel.DataAnnotations.Schema;

namespace DermaKlinik.API.Core.Entities
{
    public class GalleryImageGroupMap : AuditableEntity
    {
        public Guid ImageId { get; set; }
        public Guid GroupId { get; set; }
        public int SortOrder { get; set; }

        // Navigation Properties
        [ForeignKey("ImageId")]
        public virtual GalleryImage Image { get; set; }

        [ForeignKey("GroupId")]
        public virtual GalleryGroup Group { get; set; }
    }
}