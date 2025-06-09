using DermaKlinik.API.Application.DTOs.BlogCategory;

namespace DermaKlinik.API.Application.Services
{
    public interface IBlogCategoryService
    {
        Task<BlogCategoryDto> GetByIdAsync(Guid id);
        Task<IEnumerable<BlogCategoryDto>> GetAllAsync();
        Task<BlogCategoryDto> CreateAsync(CreateBlogCategoryDto createBlogCategoryDto);
        Task<BlogCategoryDto> UpdateAsync(Guid id, UpdateBlogCategoryDto updateBlogCategoryDto);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<BlogCategoryDto>> GetActiveCategoriesAsync();
    }
}