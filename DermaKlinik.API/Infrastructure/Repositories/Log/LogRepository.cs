using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class LogRepository(ApplicationDbContext context) : GenericRepository<Log>(context), ILogRepository
    {
    }
}