using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Core.Models;

namespace DermaKlinik.API.Application.Services
{
    public interface IBlogService
    {
        Task<BlogDto> GetByIdAsync(Guid id, Guid? languageId = null);
        Task<List<BlogDto>> GetAllAsync(PagingRequestModel request, Guid? categoryId = null, Guid? languageId = null);
        Task<BlogDto> CreateAsync(CreateBlogDto createBlogDto);
        Task<BlogTranslationDto> CreateTranslationAsync(CreateBlogTranslationDto createBlogTranslationDto);
        Task<BlogDto> UpdateAsync(Guid id, UpdateBlogDto updateBlogDto);
        Task<BlogTranslationDto> UpdateTranslationAsync(Guid id, UpdateBlogTranslationDto updateBlogTranslationDto);
        Task DeleteAsync(Guid id);
        Task HardDeleteAsync(Guid id);
        Task IncrementViewCountAsync(Guid id);
        Task<BlogDto> GetBySlugAsync(string slug, Guid? languageId = null);
    }
}