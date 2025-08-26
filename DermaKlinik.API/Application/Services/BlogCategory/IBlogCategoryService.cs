using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Core.Models;

namespace DermaKlinik.API.Application.Services
{
    public interface IBlogCategoryService
    {
        Task<BlogCategoryDto> GetByIdAsync(Guid id);
        Task<List<BlogCategoryDto>> GetAllAsync(PagingRequestModel request);
        Task<BlogCategoryDto> CreateAsync(CreateBlogCategoryDto createBlogCategoryDto);
        Task<BlogCategoryTranslationDto> CreateTranslationAsync(CreateBlogCategoryTranslationDto createBlogCategoryTranslationDto);
        Task<BlogCategoryDto> UpdateAsync(Guid id, UpdateBlogCategoryDto updateBlogCategoryDto);
        Task<BlogCategoryTranslationDto> UpdateTranslationAsync(Guid id, UpdateBlogCategoryTranslationDto updateBlogCategoryTranslationDto);
        Task DeleteAsync(Guid id);
        Task HardDeleteAsync(Guid id);
    }
}