using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Core.Models;

namespace DermaKlinik.API.Application.Services
{
    public interface IGalleryImageService
    {
        Task<GalleryImageDto> GetByIdAsync(Guid id);
        Task<List<GalleryImageDto>> GetAllAsync(PagingRequestModel request, Guid? groupId = null);
        Task<GalleryImageDto> CreateAsync(CreateGalleryImageDto createGalleryImageDto);
        Task<GalleryImageDto> UpdateAsync(Guid id, UpdateGalleryImageDto updateGalleryImageDto);
        Task DeleteAsync(Guid id);
        Task HardDeleteAsync(Guid id);
        Task AddToGroupAsync(Guid imageId, Guid groupId, int sortOrder = 0);
        Task RemoveFromGroupAsync(Guid imageId, Guid groupId);
        Task UpdateImageOrderAsync(Guid imageId, Guid groupId, int newSortOrder);
    }
}