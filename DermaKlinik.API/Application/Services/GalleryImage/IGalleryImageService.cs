using DermaKlinik.API.Application.DTOs.GalleryImage;

namespace DermaKlinik.API.Application.Services
{
    public interface IGalleryImageService
    {
        Task<GalleryImageDto> GetByIdAsync(Guid id);
        Task<IEnumerable<GalleryImageDto>> GetAllAsync();
        Task<GalleryImageDto> CreateAsync(CreateGalleryImageDto createGalleryImageDto);
        Task<GalleryImageDto> UpdateAsync(Guid id, UpdateGalleryImageDto updateGalleryImageDto);
        Task DeleteAsync(Guid id);
    }
}