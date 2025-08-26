using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Core.Models;

namespace DermaKlinik.API.Application.Services
{
    public interface IGalleryGroupService
    {
        Task<GalleryGroupDto> GetByIdAsync(Guid id);
        Task<List<GalleryGroupDto>> GetAllAsync(PagingRequestModel request);
        Task<GalleryGroupDto> CreateAsync(CreateGalleryGroupDto createGalleryGroupDto);
        Task<GalleryGroupDto> UpdateAsync(Guid id, UpdateGalleryGroupDto updateGalleryGroupDto);
        Task DeleteAsync(Guid id);
        Task HardDeleteAsync(Guid id);
        Task<List<GalleryImageDto>> GetImagesByGroupAsync(Guid groupId, PagingRequestModel request);
    }
}