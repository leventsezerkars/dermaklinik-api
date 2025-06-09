using AutoMapper;
using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Application.Services
{
    public class BlogService(IUnitOfWork unitOfWork, IMapper mapper, IBlogRepository blogRepository) : IBlogService
    {
        public async Task<List<BlogDto>> GetAllAsync()
        {
            var blogs = await blogRepository.Query()
                .Include(b => b.Category)
                .Include(b => b.Creator)
                .Include(b => b.Updater)
                .Include(b => b.Translations)
                .ToListAsync();

            return mapper.Map<List<BlogDto>>(blogs);
        }

        public async Task<BlogDto> GetByIdAsync(Guid id)
        {
            var blog = await blogRepository.Query()
                .Include(b => b.Category)
                .Include(b => b.Creator)
                .Include(b => b.Updater)
                .Include(b => b.Translations)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null)
            {
                throw new KeyNotFoundException($"Blog with ID {id} not found.");
            }

            return mapper.Map<BlogDto>(blog);
        }

        public async Task<BlogDto> GetBySlugAsync(string slug, string languageCode)
        {
            var blog = await blogRepository.Query()
                .Include(b => b.Category)
                .Include(b => b.Creator)
                .Include(b => b.Updater)
                .Include(b => b.Translations)
                .FirstOrDefaultAsync(b => b.Translations.Any(t => t.Slug == slug && t.Language.Code == languageCode));

            if (blog == null)
            {
                throw new KeyNotFoundException($"Blog with slug {slug} and language {languageCode} not found.");
            }

            return mapper.Map<BlogDto>(blog);
        }

        public async Task<BlogDto> CreateAsync(CreateBlogDto createBlogDto, Guid userId)
        {
            var blog = mapper.Map<Core.Entities.Blog>(createBlogDto);
            blog.CreatedById = userId;

            await blogRepository.AddAsync(blog);
            await unitOfWork.CompleteAsync();

            return await GetByIdAsync(blog.Id);
        }

        public async Task<BlogDto> UpdateAsync(UpdateBlogDto updateBlogDto, Guid userId)
        {
            var blog = await blogRepository.GetByIdAsync(updateBlogDto.Id);
            if (blog == null)
            {
                throw new KeyNotFoundException($"Blog with ID {updateBlogDto.Id} not found.");
            }

            mapper.Map(updateBlogDto, blog);
            blog.UpdatedById = userId;

            blogRepository.Update(blog);
            await unitOfWork.CompleteAsync();

            return await GetByIdAsync(blog.Id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var blog = await blogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                throw new KeyNotFoundException($"Blog with ID {id} not found.");
            }

            blogRepository.SoftDelete(blog);
            await unitOfWork.CompleteAsync();
        }

        public async Task IncrementViewCountAsync(Guid id)
        {
            var blog = await blogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                throw new KeyNotFoundException($"Blog with ID {id} not found.");
            }

            blog.ViewCount++;
            blogRepository.Update(blog);
            await unitOfWork.CompleteAsync();
        }

        public async Task<List<BlogDto>> GetByCategoryIdAsync(Guid categoryId)
        {
            var blogs = await blogRepository.Query()
                .Include(b => b.Category)
                .Include(b => b.Creator)
                .Include(b => b.Updater)
                .Include(b => b.Translations)
                .Where(b => b.CategoryId == categoryId)
                .ToListAsync();

            return mapper.Map<List<BlogDto>>(blogs);
        }

        public async Task<List<BlogDto>> GetByLanguageCodeAsync(Guid languageCode)
        {
            var blogs = await blogRepository.Query()
                .Include(b => b.Category)
                .Include(b => b.Creator)
                .Include(b => b.Updater)
                .Include(b => b.Translations)
                .Where(b => b.Translations.Any(t => t.Language.Id == languageCode))
                .ToListAsync();

            return mapper.Map<List<BlogDto>>(blogs);
        }
    }
}