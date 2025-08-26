using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public interface IBlogCategoryRepository : IGenericRepository<BlogCategory>
    {
        Task<List<BlogCategory>> GetAllAsync(PagingRequestModel request);
    }
}