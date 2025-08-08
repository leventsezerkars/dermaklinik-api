using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public interface ICompanyInfoRepository : IGenericRepository<CompanyInfo>
    {
        Task<CompanyInfo?> GetActiveCompanyInfoAsync();
        Task<bool> IsNameUniqueAsync(string name, Guid? excludeId = null);
        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null);
    }
}