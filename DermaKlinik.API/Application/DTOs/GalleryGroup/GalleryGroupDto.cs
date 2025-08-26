using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Application.DTOs.GalleryImage;

namespace DermaKlinik.API.Application.DTOs.GalleryGroup
{
    public class GalleryGroupDto : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeletable { get; set; }
        public List<GalleryImageDto> Images { get; set; }
        public int ImageCount { get; set; }
    }
}