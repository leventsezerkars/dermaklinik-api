using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;

namespace DermaKlinik.API.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Patient>> GetActivePatientsAsync()
        {
            return await _patientRepository.GetActivePatientsAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _patientRepository.GetByIdAsync(id);
        }

        public async Task<Patient?> GetPatientByEmailAsync(string email)
        {
            return await _patientRepository.GetPatientByEmailAsync(email);
        }

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            patient.CreatedAt = DateTime.UtcNow;
            patient.IsActive = true;

            await _patientRepository.AddAsync(patient);
            return patient;
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            var existingPatient = await _patientRepository.GetByIdAsync(patient.Id);
            if (existingPatient == null)
                throw new KeyNotFoundException($"Patient with ID {patient.Id} not found.");

            patient.UpdatedAt = DateTime.UtcNow;
            await _patientRepository.UpdateAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID {id} not found.");

            patient.IsActive = false;
            patient.UpdatedAt = DateTime.UtcNow;
            await _patientRepository.UpdateAsync(patient);
        }
    }
} 