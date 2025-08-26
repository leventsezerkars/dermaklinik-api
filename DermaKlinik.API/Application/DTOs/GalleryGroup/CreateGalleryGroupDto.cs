namespace DermaKlinik.API.Application.DTOs.GalleryGroup
{
    public class CreateGalleryGroupDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeletable { get; set; }
        public bool IsActive { get; set; }
    }
}