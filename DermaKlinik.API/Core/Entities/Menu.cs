using System.ComponentModel.DataAnnotations.Schema;

namespace DermaKlinik.API.Core.Entities
{
    public class Menu : AuditableEntity
    {
        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Menu? Parent { get; set; }
        public int SortOrder { get; set; }
        public string Slug { get; set; }
        public bool IsDeletable { get; set; }
        public int Type { get; set; }
        public string Target { get; set; }
        public ICollection<Menu> Children { get; set; } = new List<Menu>();
        public ICollection<MenuTranslation> Translations { get; set; }
    }
}