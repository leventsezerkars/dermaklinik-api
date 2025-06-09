using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Models;
using System.Linq.Expressions;

namespace DermaKlinik.API.Core.Interfaces
{
    public interface IGenericRepository<T> where T : AuditableEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null, PagingRequestModel? pagingRequest = null);
        PagedList<T> GetAllWithPaging(Expression<Func<T, bool>>? expression = null, PagingRequestModel? pagingRequest = null);
        IQueryable<T> Query();
        Task AddAsync(T entity);
        Task AddRangeAsync(IList<T> list);
        void Update(T entity);
        void SoftDelete(T entity);
        void HardDelete(T entity);
        Task SoftDeleteRangeAsync(Expression<Func<T, bool>>? expression = null);
        Task<bool> ExistsAsync(Guid id);
    }

}