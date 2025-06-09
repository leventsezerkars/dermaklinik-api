using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.Menu
{
    public class CreateMenuDto
    {
        public Guid? ParentId { get; set; }
        public int SortOrder { get; set; }
        public string Slug { get; set; }
        public bool IsDeletable { get; set; }
        public MenuType Type { get; set; }
        public string Target { get; set; }
        public List<CreateMenuTranslationDto> Translations { get; set; }
    }
}