using DermaKlinik.API.Application.DTOs.Language;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.Language
{
    public class UpdateLanguageDtoValidator : AbstractValidator<UpdateLanguageDto>
    {
        public UpdateLanguageDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Dil kodu zorunludur")
                .MaximumLength(10).WithMessage("Dil kodu en fazla 10 karakter olabilir")
                .Matches(@"^[a-z]{2,3}$").WithMessage("Geçerli bir dil kodu giriniz (2-3 karakter, sadece küçük harf)");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Dil adı zorunludur")
                .MaximumLength(50).WithMessage("Dil adı en fazla 50 karakter olabilir")
                .MinimumLength(2).WithMessage("Dil adı en az 2 karakter olmalıdır");
        }
    }
}
