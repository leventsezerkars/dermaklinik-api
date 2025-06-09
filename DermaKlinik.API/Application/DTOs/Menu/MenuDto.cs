using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.Menu
{
    public class MenuDto : AuditableEntity
    {
        public Guid? ParentId { get; set; }
        public int SortOrder { get; set; }
        public string Slug { get; set; }
        public bool IsDeletable { get; set; }
        public MenuType Type { get; set; }
        public string Target { get; set; }
        public List<MenuDto> Children { get; set; }
        public List<MenuTranslationDto> Translations { get; set; }
    }

    public enum MenuType
    {
        Page = 0,
        Link = 1,
        Content = 2,
        Anchor = 3
    }
}