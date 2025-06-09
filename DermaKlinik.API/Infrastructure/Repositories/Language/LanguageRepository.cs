using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class LanguageRepository(ApplicationDbContext context) : GenericRepository<Language>(context), ILanguageRepository
    {
    }
}