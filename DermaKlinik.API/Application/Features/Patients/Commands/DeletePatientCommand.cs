using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Interfaces;
using MediatR;

namespace DermaKlinik.API.Application.Features.Patients.Commands
{
    public class DeletePatientCommand : IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }

        public DeletePatientCommand(int id)
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
            try
            {
                await _patientService.DeletePatientAsync(request.Id);
                return ApiResponse<bool>.SuccessResult(true, "Hasta başarıyla silindi");
            }
            catch (KeyNotFoundException)
            {
                return ApiResponse<bool>.ErrorResult($"Hasta bulunamadı: ID {request.Id}", 404);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult($"Hasta silinirken bir hata oluştu: {ex.Message}");
            }
        }
    }
} 