using DermaKlinik.API.Application.DTOs.Blog;

namespace DermaKlinik.API.Application.Services
{
    public interface IBlogService
    {
        Task<List<BlogDto>> GetAllAsync();
        Task<BlogDto> GetByIdAsync(Guid id);
        Task<BlogDto> GetBySlugAsync(string slug, string languageCode);
        Task<BlogDto> CreateAsync(CreateBlogDto createBlogDto, Guid userId);
        Task<BlogDto> UpdateAsync(UpdateBlogDto updateBlogDto, Guid userId);
        Task DeleteAsync(Guid id);
        Task IncrementViewCountAsync(Guid id);
        Task<List<BlogDto>> GetByCategoryIdAsync(Guid categoryId);
        Task<List<BlogDto>> GetByLanguageCodeAsync(Guid languageCode);
    }
}