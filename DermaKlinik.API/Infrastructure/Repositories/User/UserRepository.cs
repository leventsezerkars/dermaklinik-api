using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
    {
    }
}