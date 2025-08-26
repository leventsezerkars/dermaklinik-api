namespace DermaKlinik.API.Application.DTOs.GalleryImage
{
    public class UpdateGalleryImageDto
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string AltText { get; set; }
        public string Caption { get; set; }
        public bool IsActive { get; set; }
        public List<Guid> GroupIds { get; set; }
    }
}