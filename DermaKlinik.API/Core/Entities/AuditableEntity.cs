using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DermaKlinik.API.Core.Entities
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedById { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User Creator { get; set; }

        [ForeignKey("UpdatedById")]
        public virtual User Updater { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}