using DermaKlinik.API.Application.Features.Language.Commands;
using DermaKlinik.API.Application.Features.Language.Commands.UpdateLanguage;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.Language
{
    public class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
    {
        public UpdateLanguageCommandValidator()
        {
            RuleFor(x => x.UpdateLanguageDto)
                .NotNull().WithMessage("Dil verisi zorunludur");

            When(x => x.UpdateLanguageDto != null, () =>
            {
                RuleFor(x => x.UpdateLanguageDto)
                    .SetValidator(new UpdateLanguageDtoValidator());
            });
        }
    }
}
