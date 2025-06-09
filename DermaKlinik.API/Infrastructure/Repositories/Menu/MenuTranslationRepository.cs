using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class MenuTranslationRepository(ApplicationDbContext context) : GenericRepository<MenuTranslation>(context), IMenuTranslationRepository
    {
    }
}