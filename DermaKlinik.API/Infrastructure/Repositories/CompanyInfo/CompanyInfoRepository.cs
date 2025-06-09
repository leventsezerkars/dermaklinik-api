using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class CompanyInfoRepository(ApplicationDbContext context) : GenericRepository<CompanyInfo>(context), ICompanyInfoRepository
    {
    }
}