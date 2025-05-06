using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Core.Interfaces
{
    public interface IMenuService
    {
        Task<PaginatedList<Menu>> GetAllMenusAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Menu>> GetRootMenusAsync();
        Task<IEnumerable<Menu>> GetActiveMenusAsync();
        Task<Menu?> GetMenuByIdAsync(Guid id);
        Task<Menu?> GetMenuWithChildrenAsync(Guid id);
        Task<IEnumerable<Menu>> GetMenusByPermissionAsync(string permission);
        Task<Menu> CreateMenuAsync(Menu menu);
        Task UpdateMenuAsync(Menu menu);
        Task DeleteMenuAsync(Guid id);
        Task HardDeleteMenuAsync(Guid id);
    }
} 