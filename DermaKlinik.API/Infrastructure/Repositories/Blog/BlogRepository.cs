using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class BlogRepository(ApplicationDbContext context) : GenericRepository<Blog>(context), IBlogRepository
    {
    }
}