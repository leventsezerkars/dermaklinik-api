using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Application.DTOs.GalleryGroup;

namespace DermaKlinik.API.Application.DTOs.GalleryImage
{
    public class GalleryImageDto : AuditableEntity
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string? TitleEn { get; set; }
        public string AltText { get; set; }
        public string Caption { get; set; }
        public List<GalleryGroupDto> Groups { get; set; }

        public int SortOrder { get; set; }

    }
}