using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DermaKlinik.API.Core.Entities
{
    public class Menu : AuditableEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Icon { get; set; }

        [Required]
        [StringLength(200)]
        public string Url { get; set; }

        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Menu? Parent { get; set; }

        public ICollection<Menu> Children { get; set; } = new List<Menu>();

        public int Order { get; set; }

        public bool IsVisible { get; set; } = true;

        public string? RequiredPermission { get; set; }
    }
} 