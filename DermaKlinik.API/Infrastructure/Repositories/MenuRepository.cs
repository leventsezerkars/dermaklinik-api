using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Menu>> GetRootMenusAsync()
        {
            return await _dbSet
                .Where(m => m.ParentId == null && !m.IsDeleted)
                .OrderBy(m => m.Order)
                .ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetActiveMenusAsync()
        {
            return await _dbSet
                .Where(m => m.IsActive && !m.IsDeleted)
                .OrderBy(m => m.Order)
                .ToListAsync();
        }

        public async Task<Menu?> GetMenuWithChildrenAsync(Guid id)
        {
            return await _dbSet
                .Include(m => m.Children.Where(c => !c.IsDeleted))
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        }

        public async Task<IEnumerable<Menu>> GetMenusByPermissionAsync(string permission)
        {
            return await _dbSet
                .Where(m => m.RequiredPermission == permission && m.IsActive && !m.IsDeleted)
                .OrderBy(m => m.Order)
                .ToListAsync();
        }

    }
} 