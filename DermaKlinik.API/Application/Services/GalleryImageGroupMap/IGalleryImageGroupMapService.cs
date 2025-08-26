using DermaKlinik.API.Application.DTOs.GalleryImageGroupMap;
using DermaKlinik.API.Core.Models;

namespace DermaKlinik.API.Application.Services
{
    public interface IGalleryImageGroupMapService
    {
        Task<GalleryImageGroupMapDto> GetByIdAsync(Guid id);
        Task<List<GalleryImageGroupMapDto>> GetAllAsync(PagingRequestModel request);
        Task<GalleryImageGroupMapDto> CreateAsync(CreateGalleryImageGroupMapDto createDto);
        Task<GalleryImageGroupMapDto> UpdateAsync(Guid id, UpdateGalleryImageGroupMapDto updateDto);
        Task DeleteAsync(Guid id);
        Task HardDeleteAsync(Guid id);
        Task<List<GalleryImageGroupMapDto>> GetByImageIdAsync(Guid imageId);
        Task<List<GalleryImageGroupMapDto>> GetByGroupIdAsync(Guid groupId);
        Task UpdateSortOrderAsync(Guid imageId, Guid groupId, int newSortOrder);
    }
}
