using FluentValidation;
using DermaKlinik.API.Application.Features.Patients.Queries;

namespace DermaKlinik.API.Application.Features.Patients.Queries.Validators
{
    public class GetPatientByIdQueryValidator : AbstractValidator<GetPatientByIdQuery>
    {
        public GetPatientByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Hasta ID'si boş olamaz");
        }
    }
} 