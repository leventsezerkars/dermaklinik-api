using AutoMapper;
using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Repositories;

namespace DermaKlinik.API.Application.Services
{
    public class GalleryImageService(IUnitOfWork unitOfWork, IMapper mapper, IGalleryImageRepository galleryImageRepository) : IGalleryImageService
    {
        public async Task<GalleryImageDto> GetByIdAsync(Guid id)
        {
            var galleryImage = await galleryImageRepository.GetByIdAsync(id);
            return mapper.Map<GalleryImageDto>(galleryImage);
        }

        public async Task<IEnumerable<GalleryImageDto>> GetAllAsync()
        {
            var galleryImages = galleryImageRepository.GetAll();
            return mapper.Map<IEnumerable<GalleryImageDto>>(galleryImages);
        }

        public async Task<GalleryImageDto> CreateAsync(CreateGalleryImageDto createGalleryImageDto)
        {
            var galleryImage = mapper.Map<Core.Entities.GalleryImage>(createGalleryImageDto);
            await galleryImageRepository.AddAsync(galleryImage);
            await unitOfWork.CompleteAsync();
            return mapper.Map<GalleryImageDto>(galleryImage);
        }

        public async Task<GalleryImageDto> UpdateAsync(Guid id, UpdateGalleryImageDto updateGalleryImageDto)
        {
            var galleryImage = await galleryImageRepository.GetByIdAsync(id);
            if (galleryImage == null)
            {
                return null;
            }

            mapper.Map(updateGalleryImageDto, galleryImage);
            galleryImageRepository.Update(galleryImage);
            await unitOfWork.CompleteAsync();
            return mapper.Map<GalleryImageDto>(galleryImage);
        }

        public async Task DeleteAsync(Guid id)
        {
            var galleryImage = await galleryImageRepository.GetByIdAsync(id);
            if (galleryImage != null)
            {
                galleryImageRepository.SoftDelete(galleryImage);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}