using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Interfaces;
using MediatR;

namespace DermaKlinik.API.Application.Features.Patients.Commands
{
    public class DeletePatientCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }

        public DeletePatientCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, ApiResponse<bool>>
    {
        private readonly IPatientService _patientService;

        public DeletePatientCommandHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<ApiResponse<bool>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var result = await _patientService.DeleteAsync(request.Id);
            if (!result)
            {
                return ApiResponse<bool>.ErrorResult("Hasta silinemedi");
            }

            return ApiResponse<bool>.SuccessResult(true);
        }
    }
} 