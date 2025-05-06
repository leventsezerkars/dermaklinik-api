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
        void Update(T entity);
        void Delete(T entity);
        void HardDelete(T entity);
        Task<bool> ExistsAsync(Guid id);
    }
} 