using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using MediatR;

namespace DermaKlinik.API.Application.Features.Patients.Queries
{
    public class GetPatientByIdQuery : IRequest<ApiResponse<Patient>>
    {
        public Guid Id { get; set; }

        public GetPatientByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, ApiResponse<Patient>>
    {
        private readonly IPatientService _patientService;

        public GetPatientByIdQueryHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<ApiResponse<Patient>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetByIdAsync(request.Id);
            
            if (patient == null)
                return ApiResponse<Patient>.ErrorResult("Hasta bulunamadı");

            return ApiResponse<Patient>.SuccessResult(patient);
        }
    }
} 