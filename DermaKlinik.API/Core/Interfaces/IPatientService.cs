using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Core.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<IEnumerable<Patient>> GetActivePatientsAsync();
        Task<Patient?> GetPatientByIdAsync(Guid id);
        Task<Patient?> GetPatientByEmailAsync(string email);
        Task<Patient> CreatePatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(Guid id);
        Task<Patient?> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
} 