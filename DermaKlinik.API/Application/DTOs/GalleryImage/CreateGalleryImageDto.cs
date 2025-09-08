using Microsoft.AspNetCore.Http;

namespace DermaKlinik.API.Application.DTOs.GalleryImage
{
    public class CreateGalleryImageDto
    {
        public IFormFile ImageFile { get; set; }
        public string Title { get; set; }
        public string AltText { get; set; }
        public string Caption { get; set; }
        public bool IsActive { get; set; }
        public List<Guid> GroupIds { get; set; }
    }
} 