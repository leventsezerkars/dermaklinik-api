using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.GalleryGroup
{
    public class GalleryGroupDto : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeletable { get; set; }
        public List<GalleryImageDto> Images { get; set; }
    }
}