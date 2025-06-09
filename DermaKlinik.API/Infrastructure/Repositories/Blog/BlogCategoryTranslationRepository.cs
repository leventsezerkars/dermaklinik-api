using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class BlogCategoryTranslationRepository(ApplicationDbContext context) : GenericRepository<BlogCategoryTranslation>(context), IBlogCategoryTranslationRepository
    {
    }
}