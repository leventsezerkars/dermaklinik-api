using DermaKlinik.API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Core.Interfaces
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        Task<IEnumerable<Menu>> GetRootMenusAsync();
        Task<IEnumerable<Menu>> GetActiveMenusAsync();
        Task<Menu?> GetMenuWithChildrenAsync(Guid id);
        Task<IEnumerable<Menu>> GetMenusByPermissionAsync(string permission);
    }
} 