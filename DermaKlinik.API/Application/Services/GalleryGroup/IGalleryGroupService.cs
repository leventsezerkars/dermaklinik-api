using DermaKlinik.API.Application.DTOs.GalleryGroup;

namespace DermaKlinik.API.Application.Services
{
    public interface IGalleryGroupService
    {
        Task<GalleryGroupDto> GetByIdAsync(Guid id);
        Task<IEnumerable<GalleryGroupDto>> GetAllAsync();
        Task<GalleryGroupDto> CreateAsync(CreateGalleryGroupDto createGalleryGroupDto);
        Task<GalleryGroupDto> UpdateAsync(Guid id, UpdateGalleryGroupDto updateGalleryGroupDto);
        Task DeleteAsync(Guid id);
    }
}