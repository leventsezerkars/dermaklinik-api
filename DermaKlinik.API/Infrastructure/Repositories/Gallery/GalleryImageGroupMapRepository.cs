using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class GalleryImageGroupMapRepository(ApplicationDbContext context) : GenericRepository<GalleryImageGroupMap>(context), IGalleryImageGroupMapRepository
    {
    }
}