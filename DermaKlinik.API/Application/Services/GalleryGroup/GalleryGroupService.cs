using AutoMapper;
using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Infrastructure.Repositories;
using DermaKlinik.API.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Application.Services
{
    public class GalleryGroupService : IGalleryGroupService
    {
        private readonly IGalleryGroupRepository _galleryGroupRepository;
        private readonly IGalleryImageGroupMapService _mapService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GalleryGroupService(
            IGalleryGroupRepository galleryGroupRepository, 
            IGalleryImageGroupMapService mapService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _galleryGroupRepository = galleryGroupRepository;
            _mapService = mapService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GalleryGroupDto> GetByIdAsync(Guid id)
        {
            var group = await _galleryGroupRepository.GetByIdAsync(id);
            if (group == null)
                throw new Exception("Grup bulunamadı");

            var images = await _mapService.GetByGroupIdAsync(id);
            var imageDtos = images.Select(i => _mapper.Map<GalleryImageDto>(i.Image)).ToList();

            var groupDto = _mapper.Map<GalleryGroupDto>(group);
            groupDto.Images = imageDtos;
            groupDto.ImageCount = imageDtos.Count;

            return groupDto;
        }

        public async Task<List<GalleryGroupDto>> GetAllAsync(PagingRequestModel request)
        {
            var groups = await _galleryGroupRepository.GetAll()
                .Skip((request.Page - 1) * request.Take)
                .Take(request.Take)
                .ToListAsync();

            var result = new List<GalleryGroupDto>();
            foreach (var group in groups)
            {
                var images = await _mapService.GetByGroupIdAsync(group.Id);
                var imageDtos = images.Select(i => _mapper.Map<GalleryImageDto>(i.Image)).ToList();

                var groupDto = _mapper.Map<GalleryGroupDto>(group);
                groupDto.Images = imageDtos;
                groupDto.ImageCount = imageDtos.Count;
                result.Add(groupDto);
            }

            return result;
        }

        public async Task<GalleryGroupDto> CreateAsync(CreateGalleryGroupDto createDto)
        {
            var group = _mapper.Map<GalleryGroup>(createDto);

            await _galleryGroupRepository.AddAsync(group);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(group.Id);
        }

        public async Task<GalleryGroupDto> UpdateAsync(Guid id, UpdateGalleryGroupDto updateDto)
        {
            var group = await _galleryGroupRepository.GetByIdAsync(id);
            if (group == null)
                throw new Exception("Grup bulunamadı");

            if (!group.IsDeletable && updateDto.IsDeletable)
                throw new Exception("Bu grup silinemez");

            _mapper.Map(updateDto, group);
            group.UpdatedAt = DateTime.UtcNow;

            _galleryGroupRepository.Update(group);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var group = await _galleryGroupRepository.GetByIdAsync(id);
            if (group == null)
                throw new Exception("Grup bulunamadı");

            if (!group.IsDeletable)
                throw new Exception("Bu grup silinemez");

            group.IsActive = false;
            group.UpdatedAt = DateTime.UtcNow;

            _galleryGroupRepository.Update(group);
            await _unitOfWork.CompleteAsync();
        }

        public async Task HardDeleteAsync(Guid id)
        {
            var group = await _galleryGroupRepository.GetByIdAsync(id);
            if (group == null)
                throw new Exception("Grup bulunamadı");

            if (!group.IsDeletable)
                throw new Exception("Bu grup silinemez");

            // Grup ilişkilerini sil
            var existingMaps = await _mapService.GetByGroupIdAsync(id);
            foreach (var map in existingMaps)
            {
                await _mapService.HardDeleteAsync(map.Id);
            }

            _galleryGroupRepository.HardDelete(group);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<GalleryImageDto>> GetImagesByGroupAsync(Guid groupId, PagingRequestModel request)
        {
            var maps = await _mapService.GetByGroupIdAsync(groupId);
            var images = maps
                .OrderBy(m => m.SortOrder)
                .Skip((request.Page - 1) * request.Take)
                .Take(request.Take)
                .Select(m => _mapper.Map<GalleryImageDto>(m.Image))
                .ToList();

            return images;
        }
    }
}