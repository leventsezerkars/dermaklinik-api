using FluentValidation;
using DermaKlinik.API.Application.Features.Patients.Queries;

namespace DermaKlinik.API.Application.Features.Patients.Queries.Validators
{
    public class GetPatientByIdQueryValidator : AbstractValidator<GetPatientByIdQuery>
    {
        public GetPatientByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ID giriniz");
        }
    }
} 