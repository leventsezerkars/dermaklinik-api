using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Patients.Queries
{
    public class GetActivePatientsQuery : IRequest<ApiResponse<IEnumerable<Patient>>>
    {
    }

    public class GetActivePatientsQueryHandler : IRequestHandler<GetActivePatientsQuery, ApiResponse<IEnumerable<Patient>>>
    {
        private readonly IPatientService _patientService;

        public GetActivePatientsQueryHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<ApiResponse<IEnumerable<Patient>>> Handle(GetActivePatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _patientService.GetActivePatientsAsync();
            return ApiResponse<IEnumerable<Patient>>.SuccessResult(patients, "Aktif hastalar başarıyla getirildi");
        }
    }
} 