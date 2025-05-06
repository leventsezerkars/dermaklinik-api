using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByEmailAndPasswordAsync(string email, string passwordHash);
        Task<IEnumerable<User>> GetActiveUsersAsync();
        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null);
    }
} 