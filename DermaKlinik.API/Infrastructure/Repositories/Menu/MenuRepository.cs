using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class MenuRepository(ApplicationDbContext context) : GenericRepository<Menu>(context), IMenuRepository
    {
    }
}