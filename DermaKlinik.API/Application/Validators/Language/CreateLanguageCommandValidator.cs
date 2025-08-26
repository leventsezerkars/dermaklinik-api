using DermaKlinik.API.Application.Features.Language.Commands;
using DermaKlinik.API.Application.Features.Language.Commands.CreateLanguage;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.Language
{
    public class CreateLanguageCommandValidator : AbstractValidator<CreateLanguageCommand>
    {
        public CreateLanguageCommandValidator()
        {
            RuleFor(x => x.CreateLanguageDto)
                .NotNull().WithMessage("Dil verisi zorunludur");

            When(x => x.CreateLanguageDto != null, () =>
            {
                RuleFor(x => x.CreateLanguageDto)
                    .SetValidator(new CreateLanguageDtoValidator());
            });
        }
    }
}
