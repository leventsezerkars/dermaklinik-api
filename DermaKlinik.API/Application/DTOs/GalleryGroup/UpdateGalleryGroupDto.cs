namespace DermaKlinik.API.Application.DTOs.GalleryGroup
{
    public class UpdateGalleryGroupDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeletable { get; set; }
        public bool IsActive { get; set; }
    }
}