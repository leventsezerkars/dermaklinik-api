using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.GalleryImageGroupMap
{
    public class GalleryImageGroupMapDto : AuditableEntity
    {
        public Guid ImageId { get; set; }
        public Guid GroupId { get; set; }
        public int SortOrder { get; set; }
        public GalleryImageDto Image { get; set; }
        public GalleryGroupDto Group { get; set; }
    }
}