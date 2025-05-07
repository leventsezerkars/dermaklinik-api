using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<DeletePatientCommandHandler> _logger;

        public DeletePatientCommandHandler(IPatientService patientService, ILogger<DeletePatientCommandHandler> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _patientService.GetByIdAsync(request.Id);
                if (patient == null)
                    return ApiResponse<bool>.ErrorResult($"Hasta bulunamadı: ID {request.Id}", 404);

                var result = await _patientService.DeleteAsync(request.Id);
                if (!result)
                {
                    _logger.LogWarning("Hasta silinemedi: ID {PatientId}", request.Id);
                    return ApiResponse<bool>.ErrorResult("Hasta silinemedi");
                }

                _logger.LogInformation("Hasta başarıyla silindi: ID {PatientId}", request.Id);
                return ApiResponse<bool>.SuccessResult(true, "Hasta başarıyla silindi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hasta silinirken hata oluştu: ID {PatientId}", request.Id);
                return ApiResponse<bool>.ErrorResult($"Hasta silinirken bir hata oluştu: {ex.Message}");
            }
        }
    }
} 