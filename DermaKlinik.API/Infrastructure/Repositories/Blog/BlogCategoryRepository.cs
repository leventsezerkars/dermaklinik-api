using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class BlogCategoryRepository(ApplicationDbContext context) : GenericRepository<BlogCategory>(context), IBlogCategoryRepository
    {
    }
}