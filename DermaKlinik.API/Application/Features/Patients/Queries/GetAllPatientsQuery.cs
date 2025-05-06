using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Patients.Queries
{
    public class GetAllPatientsQuery : IRequest<ApiResponse<IEnumerable<Patient>>>
    {
    }

    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, ApiResponse<IEnumerable<Patient>>>
    {
        private readonly IPatientService _patientService;

        public GetAllPatientsQueryHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<ApiResponse<IEnumerable<Patient>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return ApiResponse<IEnumerable<Patient>>.SuccessResult(patients, "Tüm hastalar başarıyla getirildi");
        }
    }
} 