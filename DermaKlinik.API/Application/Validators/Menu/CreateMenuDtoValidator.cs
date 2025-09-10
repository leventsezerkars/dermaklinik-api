using DermaKlinik.API.Application.DTOs.Menu;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.Menu
{
    public class CreateMenuDtoValidator : AbstractValidator<CreateMenuDto>
    {
        public CreateMenuDtoValidator()
        {
            RuleFor(x => x.Slug)
               .MaximumLength(255).WithMessage("Menü slug'ı en fazla 255 karakter olabilir")
               .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Geçerli bir slug formatı giriniz (sadece küçük harf, rakam ve tire)")
               .When(x => !string.IsNullOrEmpty(x.Slug));
            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Sıralama değeri 0'dan küçük olamaz");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Geçerli bir menü tipi seçiniz");

            RuleFor(x => x.Target)
                .MaximumLength(20).WithMessage("Hedef değeri en fazla 20 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.Target));

            // Translations opsiyonel - null veya boş olabilir
            RuleForEach(x => x.Translations)
                .SetValidator(new CreateMenuTranslationDtoValidator())
                .When(x => x.Translations != null && x.Translations.Any());
        }
    }
}
