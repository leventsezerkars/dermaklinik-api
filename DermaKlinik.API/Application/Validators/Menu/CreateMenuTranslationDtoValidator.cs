using DermaKlinik.API.Application.DTOs.Menu;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.Menu
{
    public class CreateMenuTranslationDtoValidator : AbstractValidator<CreateMenuTranslationDto>
    {
        public CreateMenuTranslationDtoValidator()
        {
            RuleFor(x => x.LanguageId)
                .NotNull().WithMessage("Dil ID'si zorunludur");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Menü başlığı zorunludur")
                .MaximumLength(255).WithMessage("Menü başlığı en fazla 255 karakter olabilir")
                .MinimumLength(2).WithMessage("Menü başlığı en az 2 karakter olmalıdır");

            RuleFor(x => x.Slug)
                .MaximumLength(255).WithMessage("Menü slug'ı en fazla 255 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.Slug));

            RuleFor(x => x.SeoTitle)
                .MaximumLength(255).WithMessage("SEO başlığı en fazla 255 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.SeoTitle));

            RuleFor(x => x.SeoDescription)
                .MaximumLength(500).WithMessage("SEO açıklaması en fazla 500 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.SeoDescription));

            RuleFor(x => x.SeoKeywords)
                .MaximumLength(500).WithMessage("SEO anahtar kelimeleri en fazla 500 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.SeoKeywords));
        }
    }
}
