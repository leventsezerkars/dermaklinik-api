using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Patient>> GetActivePatientsAsync()
        {
            return await _context.Patients
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<Patient?> GetPatientByEmailAsync(string email)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.Email == email);
        }

    }
} 