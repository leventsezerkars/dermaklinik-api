using AutoMapper;
using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Application.DTOs.GalleryImageGroupMap;
using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Infrastructure.Repositories;
using DermaKlinik.API.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Application.Services
{
    public class GalleryImageService : IGalleryImageService
    {
        private readonly IGalleryImageRepository _galleryImageRepository;
        private readonly IGalleryImageGroupMapService _mapService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;

        public GalleryImageService(
            IGalleryImageRepository galleryImageRepository,
            IGalleryImageGroupMapService mapService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IFileUploadService fileUploadService)
        {
            _galleryImageRepository = galleryImageRepository;
            _mapService = mapService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
        }

        public async Task<GalleryImageDto> GetByIdAsync(Guid id)
        {
            var image = await _galleryImageRepository.GetByIdAsync(id);
            if (image == null)
                throw new Exception("Resim bulunamadı");

            var groups = await _mapService.GetByImageIdAsync(id);
            var groupDtos = groups.Select(g => _mapper.Map<GalleryGroupDto>(g.Group)).ToList();

            var imageDto = _mapper.Map<GalleryImageDto>(image);
            imageDto.Groups = groupDtos;

            // Tam URL'yi oluştur
            imageDto.ImageUrl = await _fileUploadService.GetImageUrlAsync(image.ImageUrl);

            return imageDto;
        }

        public async Task<List<GalleryImageDto>> GetAllAsync(PagingRequestModel request, Guid? groupId = null)
        {
            var query = _galleryImageRepository.GetAll();

            if (groupId.HasValue)
            {
                query = query.Where(i => i.GroupMaps.Any(gm => gm.GroupId == groupId));
            }

            var images = await query
                .Skip((request.Page - 1) * request.Take)
                .Take(request.Take)
                .ToListAsync();

            var result = new List<GalleryImageDto>();
            foreach (var image in images)
            {
                var groups = await _mapService.GetByImageIdAsync(image.Id);
                var groupDtos = groups.Select(g => _mapper.Map<GalleryGroupDto>(g.Group)).ToList();

                var imageDto = _mapper.Map<GalleryImageDto>(image);
                foreach (var item in groupDtos)
                {
                    item.SortOrder = groups.FirstOrDefault(s => s.GroupId == item.Id)?.SortOrder ?? 0;
                }
                imageDto.Groups = groupDtos;
                // Tam URL'yi oluştur
                imageDto.ImageUrl = await _fileUploadService.GetImageUrlAsync(image.ImageUrl);

                result.Add(imageDto);
            }

            return result;
        }

        public async Task<GalleryImageDto> CreateAsync(CreateGalleryImageDto createDto)
        {
            // Resim dosyasını yükle
            var imagePath = await _fileUploadService.UploadImageAsync(createDto.ImageFile, "gallery");

            var image = new GalleryImage
            {
                ImageUrl = imagePath,
                Title = createDto.Title,
                TitleEn = createDto.TitleEn,
                AltText = createDto.AltText,
                Caption = createDto.Caption,
                IsActive = createDto.IsActive
            };

            await _galleryImageRepository.AddAsync(image);
            await _unitOfWork.CompleteAsync();

            // Gruplara ekle
            if (createDto.GroupIds != null && createDto.GroupIds.Any())
            {
                foreach (var groupId in createDto.GroupIds)
                {
                    await AddToGroupAsync(image.Id, groupId);
                }
            }

            return await GetByIdAsync(image.Id);
        }

        public async Task<GalleryImageDto> UpdateAsync(Guid id, UpdateGalleryImageDto updateDto)
        {
            var image = await _galleryImageRepository.GetByIdAsync(id);
            if (image == null)
                throw new Exception("Resim bulunamadı");

            // Eğer yeni resim dosyası yüklenmişse, eski dosyayı sil ve yeni dosyayı yükle
            if (updateDto.ImageFile != null)
            {
                // Eski dosyayı sil
                await _fileUploadService.DeleteImageAsync(image.ImageUrl);

                // Yeni dosyayı yükle
                var newImagePath = await _fileUploadService.UploadImageAsync(updateDto.ImageFile, "gallery");
                image.ImageUrl = newImagePath;
            }

            // Diğer alanları güncelle
            image.Title = updateDto.Title;
            image.TitleEn = updateDto.TitleEn;
            image.AltText = updateDto.AltText;
            image.Caption = updateDto.Caption;
            image.IsActive = updateDto.IsActive;
            image.UpdatedAt = DateTime.UtcNow;

            _galleryImageRepository.Update(image);

            // Mevcut grup ilişkilerini temizle
            var existingMaps = await _mapService.GetByImageIdAsync(id);
            foreach (var map in existingMaps)
            {
                await _mapService.HardDeleteAsync(map.Id);
            }

            // Yeni grup ilişkilerini ekle
            if (updateDto.GroupIds != null && updateDto.GroupIds.Any())
            {
                foreach (var groupId in updateDto.GroupIds)
                {
                    await AddToGroupAsync(id, groupId);
                }
            }

            await _unitOfWork.CompleteAsync();
            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var image = await _galleryImageRepository.GetByIdAsync(id);
            if (image == null)
                throw new Exception("Resim bulunamadı");

            image.IsActive = false;
            image.UpdatedAt = DateTime.UtcNow;

            _galleryImageRepository.Update(image);
            await _unitOfWork.CompleteAsync();
        }

        public async Task HardDeleteAsync(Guid id)
        {
            var image = await _galleryImageRepository.GetByIdAsync(id);
            if (image == null)
                throw new Exception("Resim bulunamadı");

            // Fiziksel dosyayı sil
            await _fileUploadService.DeleteImageAsync(image.ImageUrl);

            // Grup ilişkilerini sil
            var existingMaps = await _mapService.GetByImageIdAsync(id);
            foreach (var map in existingMaps)
            {
                await _mapService.HardDeleteAsync(map.Id);
            }

            _galleryImageRepository.HardDelete(image);
            await _unitOfWork.CompleteAsync();
        }

        public async Task AddToGroupAsync(Guid imageId, Guid groupId, int sortOrder = 0)
        {
            var existingMap = await _mapService.GetByImageIdAsync(imageId);
            if (existingMap.Any(gm => gm.GroupId == groupId))
                return; // Zaten grupta

            var groupImages = await _mapService.GetByGroupIdAsync(groupId);

            var maxOrder = groupImages.Any() ? groupImages.Max(gm => gm.SortOrder) + 1 : 1;

            var createDto = new CreateGalleryImageGroupMapDto
            {
                ImageId = imageId,
                GroupId = groupId,
                SortOrder = maxOrder
            };

            await _mapService.CreateAsync(createDto);
        }

        public async Task RemoveFromGroupAsync(Guid imageId, Guid groupId)
        {
            var existingMaps = await _mapService.GetByImageIdAsync(imageId);
            var map = existingMaps.FirstOrDefault(gm => gm.GroupId == groupId);
            if (map != null)
            {
                await _mapService.DeleteAsync(map.Id);
            }
        }

        public async Task UpdateImageOrderAsync(Guid imageId, Guid groupId, int newSortOrder)
        {
            await _mapService.UpdateSortOrderAsync(imageId, groupId, newSortOrder);
        }
    }
}