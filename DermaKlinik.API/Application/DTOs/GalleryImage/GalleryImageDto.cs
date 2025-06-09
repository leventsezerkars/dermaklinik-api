using DermaKlinik.API.Application.DTOs.GalleryImageGroupMap;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.GalleryImage
{
    public class GalleryImageDto : AuditableEntity
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string AltText { get; set; }
        public string Caption { get; set; }
        public List<GalleryImageGroupMapDto> GroupMaps { get; set; }
    }


}