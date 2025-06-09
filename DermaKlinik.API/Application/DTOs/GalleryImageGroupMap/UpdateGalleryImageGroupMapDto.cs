namespace DermaKlinik.API.Application.DTOs.GalleryImageGroupMap
{
    public class UpdateGalleryImageGroupMapDto
    {
        public Guid ImageId { get; set; }
        public Guid GroupId { get; set; }
        public int SortOrder { get; set; }
    }
}