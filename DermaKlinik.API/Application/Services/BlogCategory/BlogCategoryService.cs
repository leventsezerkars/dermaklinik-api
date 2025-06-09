using AutoMapper;
using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Repositories;

namespace DermaKlinik.API.Application.Services
{
    public class BlogCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IBlogCategoryRepository blogCategoryRepository) : IBlogCategoryService
    {
        public async Task<BlogCategoryDto> GetByIdAsync(Guid id)
        {
            var category = await blogCategoryRepository.GetByIdAsync(id);
            return mapper.Map<BlogCategoryDto>(category);
        }

        public async Task<IEnumerable<BlogCategoryDto>> GetAllAsync()
        {
            var categories = blogCategoryRepository.GetAll();
            return mapper.Map<IEnumerable<BlogCategoryDto>>(categories);
        }

        public async Task<BlogCategoryDto> CreateAsync(CreateBlogCategoryDto createBlogCategoryDto)
        {
            var category = mapper.Map<Core.Entities.BlogCategory>(createBlogCategoryDto);
            await blogCategoryRepository.AddAsync(category);
            await unitOfWork.CompleteAsync();
            return mapper.Map<BlogCategoryDto>(category);
        }

        public async Task<BlogCategoryDto> UpdateAsync(Guid id, UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            var category = await blogCategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }

            mapper.Map(updateBlogCategoryDto, category);
            blogCategoryRepository.Update(category);
            await unitOfWork.CompleteAsync();
            return mapper.Map<BlogCategoryDto>(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await blogCategoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                blogCategoryRepository.SoftDelete(category);
                await unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await blogCategoryRepository.GetByIdAsync(id) != null;
        }

        public async Task<IEnumerable<BlogCategoryDto>> GetActiveCategoriesAsync()
        {
            var categories = blogCategoryRepository.GetAll();
            return mapper.Map<IEnumerable<BlogCategoryDto>>(categories);
        }
    }
}