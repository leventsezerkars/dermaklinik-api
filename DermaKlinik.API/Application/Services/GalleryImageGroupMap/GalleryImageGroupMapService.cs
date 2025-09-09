using AutoMapper;
using DermaKlinik.API.Application.DTOs.GalleryImageGroupMap;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Infrastructure.Repositories;
using DermaKlinik.API.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Application.Services
{
    public class GalleryImageGroupMapService : IGalleryImageGroupMapService
    {
        private readonly IGalleryImageGroupMapRepository _galleryImageGroupMapRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GalleryImageGroupMapService(
            IGalleryImageGroupMapRepository galleryImageGroupMapRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _galleryImageGroupMapRepository = galleryImageGroupMapRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GalleryImageGroupMapDto> GetByIdAsync(Guid id)
        {
            var map = await _galleryImageGroupMapRepository.GetAll()
                .Include(m => m.Image)
                .Include(m => m.Group)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (map == null)
                throw new Exception("Grup-Resim eşleşmesi bulunamadı");

            return _mapper.Map<GalleryImageGroupMapDto>(map);
        }

        public async Task<List<GalleryImageGroupMapDto>> GetAllAsync(PagingRequestModel request)
        {
            var maps = await _galleryImageGroupMapRepository.GetAll()
                .Include(m => m.Image)
                .Include(m => m.Group)
                .OrderBy(m => m.SortOrder)
                .Skip((request.Page - 1) * request.Take)
                .Take(request.Take)
                .ToListAsync();

            return _mapper.Map<List<GalleryImageGroupMapDto>>(maps);
        }

        public async Task<GalleryImageGroupMapDto> CreateAsync(CreateGalleryImageGroupMapDto createDto)
        {
            var map = _mapper.Map<GalleryImageGroupMap>(createDto);

            await _galleryImageGroupMapRepository.AddAsync(map);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(map.Id);
        }

        public async Task<GalleryImageGroupMapDto> UpdateAsync(Guid id, UpdateGalleryImageGroupMapDto updateDto)
        {
            var map = await _galleryImageGroupMapRepository.GetByIdAsync(id);
            if (map == null)
                throw new Exception("Grup-Resim eşleşmesi bulunamadı");

            _mapper.Map(updateDto, map);
            map.UpdatedAt = DateTime.UtcNow;

            _galleryImageGroupMapRepository.Update(map);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var map = await _galleryImageGroupMapRepository.GetByIdAsync(id);
            if (map == null)
                throw new Exception("Grup-Resim eşleşmesi bulunamadı");

            map.IsDeleted = false;
            map.UpdatedAt = DateTime.UtcNow;

            _galleryImageGroupMapRepository.Update(map);
            await _unitOfWork.CompleteAsync();
        }

        public async Task HardDeleteAsync(Guid id)
        {
            var map = await _galleryImageGroupMapRepository.GetByIdAsync(id);
            if (map == null)
                throw new Exception("Grup-Resim eşleşmesi bulunamadı");

            _galleryImageGroupMapRepository.HardDelete(map);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<GalleryImageGroupMapDto>> GetByImageIdAsync(Guid imageId)
        {
            var maps = await _galleryImageGroupMapRepository.GetAll()
                .Include(m => m.Image)
                .Include(m => m.Group)
                .Where(m => m.ImageId == imageId && m.IsActive)
                .OrderBy(m => m.SortOrder)
                .ToListAsync();

            return _mapper.Map<List<GalleryImageGroupMapDto>>(maps);
        }

        public async Task<List<GalleryImageGroupMapDto>> GetByGroupIdAsync(Guid groupId)
        {
            var maps = await _galleryImageGroupMapRepository.GetAll()
                .Include(m => m.Image)
                .Include(m => m.Group)
                .Where(m => m.GroupId == groupId && m.IsActive)
                .OrderBy(m => m.SortOrder)
                .ToListAsync();

            return _mapper.Map<List<GalleryImageGroupMapDto>>(maps);
        }

        public async Task UpdateSortOrderAsync(Guid imageId, Guid groupId, int newSortOrder)
        {
            var map = await _galleryImageGroupMapRepository.GetAll()
                .FirstOrDefaultAsync(m => m.ImageId == imageId && m.GroupId == groupId && m.IsActive);

            if (map == null)
                throw new Exception("Grup-Resim eşleşmesi bulunamadı");

            map.SortOrder = newSortOrder;
            map.UpdatedAt = DateTime.UtcNow;

            _galleryImageGroupMapRepository.Update(map);
            await _unitOfWork.CompleteAsync();
        }
    }
}
