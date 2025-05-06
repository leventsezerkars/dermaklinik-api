using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }

        public async Task<User?> GetByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == passwordHash && !u.IsDeleted);
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _dbSet
                .Where(u => u.IsActive && !u.IsDeleted)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToListAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null)
        {
            return !await _dbSet
                .AnyAsync(u => u.Email == email && 
                             !u.IsDeleted && 
                             (!excludeUserId.HasValue || u.Id != excludeUserId.Value));
        }
    }
} 