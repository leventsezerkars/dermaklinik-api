using DermaKlinik.API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task HardDeleteAsync(T entity);
        Task<bool> ExistsAsync(Guid id);
    }
} 