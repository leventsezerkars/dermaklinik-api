using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Patients.Commands
{
    public class UpdatePatientCommand : IRequest<ApiResponse<Patient>>
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
    }

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, ApiResponse<Patient>>
    {
        private readonly IPatientService _patientService;

        public UpdatePatientCommandHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<ApiResponse<Patient>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingPatient = await _patientService.GetPatientByIdAsync(request.Id);
                if (existingPatient == null)
                    return ApiResponse<Patient>.ErrorResult($"Hasta bulunamadı: ID {request.Id}", 404);

                var emailPatient = await _patientService.GetPatientByEmailAsync(request.Email);
                if (emailPatient != null && emailPatient.Id != request.Id)
                    return ApiResponse<Patient>.ErrorResult("Bu e-posta adresi başka bir hasta tarafından kullanılıyor.");

                existingPatient.FirstName = request.FirstName;
                existingPatient.LastName = request.LastName;
                existingPatient.Email = request.Email;
                existingPatient.PhoneNumber = request.PhoneNumber;
                existingPatient.DateOfBirth = request.DateOfBirth;
                existingPatient.Address = request.Address;

                await _patientService.UpdatePatientAsync(existingPatient);
                return ApiResponse<Patient>.SuccessResult(existingPatient, "Hasta bilgileri başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return ApiResponse<Patient>.ErrorResult($"Hasta güncellenirken bir hata oluştu: {ex.Message}");
            }
        }
    }
} 