using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class BlogTranslationRepository(ApplicationDbContext context) : GenericRepository<BlogTranslation>(context), IBlogTranslationRepository
    {
    }
}