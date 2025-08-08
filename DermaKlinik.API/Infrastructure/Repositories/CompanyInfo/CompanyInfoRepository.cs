using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class CompanyInfoRepository(ApplicationDbContext context) : GenericRepository<CompanyInfo>(context), ICompanyInfoRepository
    {
        public async Task<CompanyInfo?> GetActiveCompanyInfoAsync()
        {
            return await _dbSet
                .Where(c => c.IsActive && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsNameUniqueAsync(string name, Guid? excludeId = null)
        {
            var query = _dbSet.Where(c => c.Name == name && !c.IsDeleted);
            
            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }

            return !await query.AnyAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null)
        {
            var query = _dbSet.Where(c => c.Email == email && !c.IsDeleted);
            
            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }

            return !await query.AnyAsync();
        }
    }
}