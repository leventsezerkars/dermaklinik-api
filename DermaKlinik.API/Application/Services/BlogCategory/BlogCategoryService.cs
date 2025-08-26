using AutoMapper;
using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Infrastructure.Repositories;
using DermaKlinik.API.Infrastructure.UnitOfWork;

namespace DermaKlinik.API.Application.Services
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IBlogCategoryTranslationRepository _blogCategoryTranslationRepository;
        private readonly IMapper _mapper;

        public BlogCategoryService(
            IUnitOfWork unitOfWork,
            IBlogCategoryRepository blogCategoryRepository,
            IBlogCategoryTranslationRepository blogCategoryTranslationRepository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _blogCategoryRepository = blogCategoryRepository;
            _blogCategoryTranslationRepository = blogCategoryTranslationRepository;
            _mapper = mapper;
        }

        public async Task<BlogCategoryDto> GetByIdAsync(Guid id)
        {
            var blogCategory = await _blogCategoryRepository.GetByIdAsync(id);
            if (blogCategory == null)
                throw new Exception("Blog kategorisi bulunamadı.");

            return _mapper.Map<BlogCategoryDto>(blogCategory);
        }

        public async Task<List<BlogCategoryDto>> GetAllAsync(PagingRequestModel request)
        {
            var blogCategories = await _blogCategoryRepository.GetAllAsync(request);
            return _mapper.Map<List<BlogCategoryDto>>(blogCategories);
        }

        public async Task<BlogCategoryDto> CreateAsync(CreateBlogCategoryDto createBlogCategoryDto)
        {
            var blogCategory = _mapper.Map<Core.Entities.BlogCategory>(createBlogCategoryDto);
            blogCategory.Id = Guid.NewGuid();
            blogCategory.IsActive = true;
            blogCategory.CreatedAt = DateTime.UtcNow;
            blogCategory.UpdatedAt = DateTime.UtcNow;

            await _blogCategoryRepository.AddAsync(blogCategory);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BlogCategoryDto>(blogCategory);
        }

        public async Task<BlogCategoryTranslationDto> CreateTranslationAsync(CreateBlogCategoryTranslationDto createBlogCategoryTranslationDto)
        {
            var translation = _mapper.Map<Core.Entities.BlogCategoryTranslation>(createBlogCategoryTranslationDto);
            translation.Id = Guid.NewGuid();
            translation.CreatedAt = DateTime.UtcNow;
            translation.UpdatedAt = DateTime.UtcNow;

            await _blogCategoryTranslationRepository.AddAsync(translation);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BlogCategoryTranslationDto>(translation);
        }

        public async Task<BlogCategoryDto> UpdateAsync(Guid id, UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            var blogCategory = await _blogCategoryRepository.GetByIdAsync(id);
            if (blogCategory == null)
                throw new Exception("Blog kategorisi bulunamadı.");

            blogCategory.IsActive = updateBlogCategoryDto.IsActive;
            blogCategory.UpdatedAt = DateTime.UtcNow;

            _blogCategoryRepository.Update(blogCategory);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BlogCategoryDto>(blogCategory);
        }

        public async Task<BlogCategoryTranslationDto> UpdateTranslationAsync(Guid id, UpdateBlogCategoryTranslationDto updateBlogCategoryTranslationDto)
        {
            var translation = await _blogCategoryTranslationRepository.GetByIdAsync(id);
            if (translation == null)
                throw new Exception("Blog kategorisi çevirisi bulunamadı.");

            translation.Name = updateBlogCategoryTranslationDto.Name;
            translation.UpdatedAt = DateTime.UtcNow;

            _blogCategoryTranslationRepository.Update(translation);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BlogCategoryTranslationDto>(translation);
        }

        public async Task DeleteAsync(Guid id)
        {
            var blogCategory = await _blogCategoryRepository.GetByIdAsync(id);
            if (blogCategory == null)
                throw new Exception("Blog kategorisi bulunamadı.");

            blogCategory.IsActive = false;
            blogCategory.UpdatedAt = DateTime.UtcNow;

            _blogCategoryRepository.Update(blogCategory);
            await _unitOfWork.CompleteAsync();
        }

        public async Task HardDeleteAsync(Guid id)
        {
            var blogCategory = await _blogCategoryRepository.GetByIdAsync(id);
            if (blogCategory == null)
                throw new Exception("Blog kategorisi bulunamadı.");

            _blogCategoryRepository.HardDelete(blogCategory);
            await _unitOfWork.CompleteAsync();
        }
    }
}