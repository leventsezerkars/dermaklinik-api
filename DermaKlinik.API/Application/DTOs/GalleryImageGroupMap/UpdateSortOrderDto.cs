namespace DermaKlinik.API.Application.DTOs.GalleryImageGroupMap
{
    public class UpdateSortOrderDto
    {
        public Guid ImageId { get; set; }
        public Guid GroupId { get; set; }
        public int NewSortOrder { get; set; }
    }
}
