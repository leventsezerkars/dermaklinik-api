using FluentValidation;
using DermaKlinik.API.Application.Features.Patients.Commands;

namespace DermaKlinik.API.Application.Features.Patients.Commands.Validators
{
    public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ID giriniz");
        }
    }
} 