using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class GalleryImageRepository(ApplicationDbContext context) : GenericRepository<GalleryImage>(context), IGalleryImageRepository
    {
    }
}