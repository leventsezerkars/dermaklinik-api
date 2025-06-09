using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Extensions;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : AuditableEntity
    {
        protected readonly ApplicationDbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task AddRangeAsync(IList<T> list)
        {
            await _dbSet.AddRangeAsync(list);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public async Task SoftDeleteRangeAsync(Expression<Func<T, bool>>? expression = null)
        {
            var deletedentities = GetAll(expression);
            foreach (var entity in deletedentities)
            {
                entity.IsDeleted = true;
                Update(entity);
            }
        }
        public void HardDelete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null, PagingRequestModel? pagingRequest = null)
        {
            var result = Query();
            if (expression != null)
                result = result.Where(expression);
            if (pagingRequest != null)
            {
                if (!pagingRequest.OrderBy.IsEmpty())
                    result = result.OrderByDynamic(pagingRequest.OrderBy, pagingRequest.Direction);
                if (pagingRequest.Take != 0)
                    result = result.Paging(pagingRequest.Take, pagingRequest.Page);
            }

            return result;
        }

        public PagedList<T> GetAllWithPaging(Expression<Func<T, bool>>? expression = null, PagingRequestModel? pagingRequest = null)
        {
            var result = Query();
            if (expression != null)
                result = result.Where(expression);
            pagingRequest = pagingRequest ?? new PagingRequestModel();

            if (!pagingRequest.OrderBy.IsEmpty())
                result = result.OrderByDynamic(pagingRequest.OrderBy, pagingRequest.Direction);

            var pagingresult = result.PagingWithModel(pagingRequest.Page, pagingRequest.Take);

            return pagingresult;
        }

        
    }
}