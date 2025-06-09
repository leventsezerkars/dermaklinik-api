using AutoMapper;
using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Repositories;

namespace DermaKlinik.API.Application.Services
{
    public class GalleryGroupService(IUnitOfWork unitOfWork, IMapper mapper, IGalleryGroupRepository galleryGroupRepository) : IGalleryGroupService
    {
        public async Task<GalleryGroupDto> GetByIdAsync(Guid id)
        {
            var galleryGroup = await galleryGroupRepository.GetByIdAsync(id);
            return mapper.Map<GalleryGroupDto>(galleryGroup);
        }

        public async Task<IEnumerable<GalleryGroupDto>> GetAllAsync()
        {
            var galleryGroups = galleryGroupRepository.GetAll();
            return mapper.Map<IEnumerable<GalleryGroupDto>>(galleryGroups);
        }

        public async Task<GalleryGroupDto> CreateAsync(CreateGalleryGroupDto createGalleryGroupDto)
        {
            var galleryGroup = mapper.Map<Core.Entities.GalleryGroup>(createGalleryGroupDto);
            await galleryGroupRepository.AddAsync(galleryGroup);
            await unitOfWork.CompleteAsync();
            return mapper.Map<GalleryGroupDto>(galleryGroup);
        }

        public async Task<GalleryGroupDto> UpdateAsync(Guid id, UpdateGalleryGroupDto updateGalleryGroupDto)
        {
            var galleryGroup = await galleryGroupRepository.GetByIdAsync(id);
            if (galleryGroup == null)
            {
                return null;
            }

            mapper.Map(updateGalleryGroupDto, galleryGroup);
            galleryGroupRepository.Update(galleryGroup);
            await unitOfWork.CompleteAsync();
            return mapper.Map<GalleryGroupDto>(galleryGroup);
        }

        public async Task DeleteAsync(Guid id)
        {
            var galleryGroup = await galleryGroupRepository.GetByIdAsync(id);
            if (galleryGroup != null)
            {
                galleryGroupRepository.SoftDelete(galleryGroup);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}