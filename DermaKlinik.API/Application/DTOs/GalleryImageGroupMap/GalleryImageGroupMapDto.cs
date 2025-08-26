using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Application.DTOs.GalleryGroup;

namespace DermaKlinik.API.Application.DTOs.GalleryImageGroupMap
{
    public class GalleryImageGroupMapDto : AuditableEntity
    {
        public Guid ImageId { get; set; }
        public Guid GroupId { get; set; }
        public int SortOrder { get; set; }
        
        // Navigation Properties
        public GalleryImageDto Image { get; set; }
        public GalleryGroupDto Group { get; set; }
    }
}