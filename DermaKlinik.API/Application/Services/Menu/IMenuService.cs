using DermaKlinik.API.Application.DTOs.Menu;
using DermaKlinik.API.Application.Models.FilterModels;
using DermaKlinik.API.Core.Models;

namespace DermaKlinik.API.Application.Services
{
    public interface IMenuService
    {
        Task<MenuDto> GetByIdAsync(Guid id);
        Task<List<MenuDto>> GetByParentIdAsync(Guid? parentId);
        Task<MenuDto> GetBySlugAsync(string slug);
        Task<List<MenuDto>> GetAllAsync(PagingRequestModel request, MenuFilter filters);
        Task<MenuDto> CreateAsync(CreateMenuDto createMenuDto);
        Task<MenuTranslationDto> CreateTranslationAsync(CreateMenuTranslationDto createMenuDto);
        Task<MenuDto> UpdateAsync(Guid id, UpdateMenuDto updateMenuDto);
        Task<MenuTranslationDto> UpdateTranslationAsync(Guid id, UpdateMenuTranslationDto updateMenuDto);
        Task DeleteAsync(Guid id);
        Task HardDeleteAsync(Guid id);
    }
}