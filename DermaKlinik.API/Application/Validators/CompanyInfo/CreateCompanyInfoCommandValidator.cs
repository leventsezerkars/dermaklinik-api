using DermaKlinik.API.Application.Features.CompanyInfo.Commands;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.CompanyInfo
{
    public class CreateCompanyInfoCommandValidator : AbstractValidator<CreateCompanyInfoCommand>
    {
        public CreateCompanyInfoCommandValidator()
        {
            RuleFor(x => x.CreateCompanyInfoDto)
                .NotNull().WithMessage("Şirket bilgisi zorunludur");

            When(x => x.CreateCompanyInfoDto != null, () =>
            {
                RuleFor(x => x.CreateCompanyInfoDto)
                    .SetValidator(new CreateCompanyInfoDtoValidator());
            });
        }
    }
}
