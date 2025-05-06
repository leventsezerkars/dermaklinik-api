using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Core.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetActivePatientsAsync();
        Task<Patient?> GetPatientByEmailAsync(string email);
    }
} 