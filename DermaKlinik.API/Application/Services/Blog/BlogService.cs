using AutoMapper;
using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Infrastructure.Repositories;
using DermaKlinik.API.Infrastructure.UnitOfWork;

namespace DermaKlinik.API.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogTranslationRepository _blogTranslationRepository;
        private readonly IMapper _mapper;

        public BlogService(
            IUnitOfWork unitOfWork,
            IBlogRepository blogRepository,
            IBlogTranslationRepository blogTranslationRepository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _blogRepository = blogRepository;
            _blogTranslationRepository = blogTranslationRepository;
            _mapper = mapper;
        }

        public async Task<BlogDto> GetByIdAsync(Guid id, Guid? languageId = null)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
                throw new Exception("Blog bulunamadı.");

            var blogDto = _mapper.Map<BlogDto>(blog);
            
            // Kategori bilgilerini ekle
            if (blog.Category != null)
            {
                blogDto.Category = _mapper.Map<BlogCategoryDto>(blog.Category);
            }

            if (languageId.HasValue)
            {
                var translation = blog.Translations?.FirstOrDefault(t => t.LanguageId == languageId.Value);
                if (translation != null)
                {
                    blogDto.CurrentTranslation = _mapper.Map<BlogTranslationDto>(translation);
                }
            }
            else
            {
                // Eğer languageId belirtilmemişse, ilk çeviriyi al
                var firstTranslation = blog.Translations?.FirstOrDefault();
                if (firstTranslation != null)
                {
                    blogDto.CurrentTranslation = _mapper.Map<BlogTranslationDto>(firstTranslation);
                }
            }

            return blogDto;
        }

        public async Task<List<BlogDto>> GetAllAsync(PagingRequestModel request, Guid? categoryId = null, Guid? languageId = null)
        {
            var blogs = await _blogRepository.GetAllAsync(request, categoryId, languageId);
            var blogDtos = _mapper.Map<List<BlogDto>>(blogs);

            foreach (var blogDto in blogDtos)
            {
                var blog = blogs.FirstOrDefault(b => b.Id == blogDto.Id);
                
                // Kategori bilgilerini ekle
                if (blog?.Category != null)
                {
                    blogDto.Category = _mapper.Map<BlogCategoryDto>(blog.Category);
                }

                if (languageId.HasValue)
                {
                    var translation = blog?.Translations?.FirstOrDefault(t => t.LanguageId == languageId.Value);
                    if (translation != null)
                    {
                        blogDto.CurrentTranslation = _mapper.Map<BlogTranslationDto>(translation);
                    }
                    
                    var categoryTranslation = blog?.Category?.Translations?.FirstOrDefault(t => t.LanguageId == languageId.Value);
                    if (categoryTranslation != null)
                        blogDto.CategoryName = categoryTranslation.Name;
                }
                else
                {
                    // Eğer languageId belirtilmemişse, ilk çeviriyi al
                    var firstTranslation = blog?.Translations?.FirstOrDefault();
                    if (firstTranslation != null)
                    {
                        blogDto.CurrentTranslation = _mapper.Map<BlogTranslationDto>(firstTranslation);
                    }
                    
                    var firstCategoryTranslation = blog?.Category?.Translations?.FirstOrDefault();
                    if (firstCategoryTranslation != null)
                        blogDto.CategoryName = firstCategoryTranslation.Name;
                }
            }

            return blogDtos;
        }

        public async Task<BlogDto> CreateAsync(CreateBlogDto createBlogDto)
        {
            var blog = _mapper.Map<Core.Entities.Blog>(createBlogDto);
            blog.Id = Guid.NewGuid();
            blog.ViewCount = 0;
            blog.IsActive = true;
            blog.CreatedAt = DateTime.UtcNow;
            blog.UpdatedAt = DateTime.UtcNow;

            await _blogRepository.AddAsync(blog);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BlogDto>(blog);
        }

        public async Task<BlogTranslationDto> CreateTranslationAsync(CreateBlogTranslationDto createBlogTranslationDto)
        {
            var translation = _mapper.Map<Core.Entities.BlogTranslation>(createBlogTranslationDto);
            translation.Id = Guid.NewGuid();
            translation.CreatedAt = DateTime.UtcNow;
            translation.UpdatedAt = DateTime.UtcNow;

            await _blogTranslationRepository.AddAsync(translation);
            await _unitOfWork.CompleteAsync();

            // Language bilgisini de almak için tekrar çek
            var createdTranslation = await _blogTranslationRepository.GetByIdAsync(translation.Id);
            return _mapper.Map<BlogTranslationDto>(createdTranslation);
        }

        public async Task<BlogDto> UpdateAsync(Guid id, UpdateBlogDto updateBlogDto)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
                throw new Exception("Blog bulunamadı.");

            blog.CategoryId = updateBlogDto.CategoryId;
            blog.IsActive = updateBlogDto.IsActive;
            blog.UpdatedAt = DateTime.UtcNow;

            _blogRepository.Update(blog);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BlogDto>(blog);
        }

        public async Task<BlogTranslationDto> UpdateTranslationAsync(Guid id, UpdateBlogTranslationDto updateBlogTranslationDto)
        {
            var translation = await _blogTranslationRepository.GetByIdAsync(id);
            if (translation == null)
                throw new Exception("Blog çevirisi bulunamadı.");

            translation.Title = updateBlogTranslationDto.Title;
            translation.Content = updateBlogTranslationDto.Content;
            translation.Slug = updateBlogTranslationDto.Slug;
            translation.SeoTitle = updateBlogTranslationDto.SeoTitle;
            translation.SeoDescription = updateBlogTranslationDto.SeoDescription;
            translation.SeoKeywords = updateBlogTranslationDto.SeoKeywords;
            translation.FeaturedImage = updateBlogTranslationDto.FeaturedImage ?? string.Empty;
            translation.VideoUrl = updateBlogTranslationDto.VideoUrl ?? string.Empty;
            translation.DocumentUrl = updateBlogTranslationDto.DocumentUrl ?? string.Empty;
            translation.UpdatedAt = DateTime.UtcNow;

            _blogTranslationRepository.Update(translation);
            await _unitOfWork.CompleteAsync();

            // Language bilgisini de almak için tekrar çek
            var updatedTranslation = await _blogTranslationRepository.GetByIdAsync(id);
            return _mapper.Map<BlogTranslationDto>(updatedTranslation);
        }

        public async Task DeleteAsync(Guid id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
                throw new Exception("Blog bulunamadı.");

            blog.IsActive = false;
            blog.UpdatedAt = DateTime.UtcNow;

            _blogRepository.Update(blog);
            await _unitOfWork.CompleteAsync();
        }

        public async Task HardDeleteAsync(Guid id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
                throw new Exception("Blog bulunamadı.");

            _blogRepository.HardDelete(blog);
            await _unitOfWork.CompleteAsync();
        }

        public async Task IncrementViewCountAsync(Guid id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
                throw new Exception("Blog bulunamadı.");

            blog.ViewCount++;
            blog.UpdatedAt = DateTime.UtcNow;

            _blogRepository.Update(blog);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<BlogDto> GetBySlugAsync(string slug, string? languageCode = null)
        {
            var blog = await _blogRepository.GetBySlugAsync(slug);
            if (blog == null)
                throw new Exception("Blog bulunamadı.");

            var blogDto = _mapper.Map<BlogDto>(blog);
            
            // Kategori bilgilerini ekle
            if (blog.Category != null)
            {
                blogDto.Category = _mapper.Map<BlogCategoryDto>(blog.Category);
            }

            if (!string.IsNullOrEmpty(languageCode))
            {
                var translation = blog.Translations?.FirstOrDefault(t => t.Language?.Code == languageCode);
                if (translation != null)
                {
                    blogDto.CurrentTranslation = _mapper.Map<BlogTranslationDto>(translation);
                }
                
                var categoryTranslation = blog.Category?.Translations?.FirstOrDefault(t => t.Language?.Code == languageCode);
                if (categoryTranslation != null)
                    blogDto.CategoryName = categoryTranslation.Name;
            }
            else
            {
                // Eğer languageCode belirtilmemişse, ilk çeviriyi al
                var firstTranslation = blog.Translations?.FirstOrDefault();
                if (firstTranslation != null)
                {
                    blogDto.CurrentTranslation = _mapper.Map<BlogTranslationDto>(firstTranslation);
                }
                
                var firstCategoryTranslation = blog.Category?.Translations?.FirstOrDefault();
                if (firstCategoryTranslation != null)
                    blogDto.CategoryName = firstCategoryTranslation.Name;
            }

            return blogDto;
        }
    }
}