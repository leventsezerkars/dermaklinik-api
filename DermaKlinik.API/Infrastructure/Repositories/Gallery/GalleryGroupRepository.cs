using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class GalleryGroupRepository(ApplicationDbContext context) : GenericRepository<GalleryGroup>(context), IGalleryGroupRepository
    {
    }
}