using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        Task<List<Blog>> GetAllAsync(PagingRequestModel request, Guid? categoryId = null, Guid? languageId = null);
    }
}