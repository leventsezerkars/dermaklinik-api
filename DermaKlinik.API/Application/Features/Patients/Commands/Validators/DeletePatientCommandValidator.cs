using FluentValidation;
using DermaKlinik.API.Application.Features.Patients.Commands;

namespace DermaKlinik.API.Application.Features.Patients.Commands.Validators
{
    public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Hasta ID'si boş olamaz");
        }
    }
} 