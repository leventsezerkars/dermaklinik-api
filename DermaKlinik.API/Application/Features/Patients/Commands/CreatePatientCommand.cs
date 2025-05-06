using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Patients.Commands
{
    public class CreatePatientCommand : IRequest<ApiResponse<Patient>>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
    }

    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, ApiResponse<Patient>>
    {
        private readonly IPatientService _patientService;

        public CreatePatientCommandHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<ApiResponse<Patient>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingPatient = await _patientService.GetPatientByEmailAsync(request.Email);
                if (existingPatient != null)
                    return ApiResponse<Patient>.ErrorResult("Bu e-posta adresi ile kayıtlı bir hasta zaten var.");

                var patient = new Patient
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    DateOfBirth = request.DateOfBirth,
                    Address = request.Address
                };

                var createdPatient = await _patientService.CreatePatientAsync(patient);
                return ApiResponse<Patient>.SuccessResult(createdPatient, "Hasta başarıyla oluşturuldu", 201);
            }
            catch (Exception ex)
            {
                return ApiResponse<Patient>.ErrorResult($"Hasta oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }
    }
} 