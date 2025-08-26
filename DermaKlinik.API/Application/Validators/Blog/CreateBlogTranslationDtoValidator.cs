using DermaKlinik.API.Application.DTOs.Blog;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.Blog
{
    public class CreateBlogTranslationDtoValidator : AbstractValidator<CreateBlogTranslationDto>
    {
        public CreateBlogTranslationDtoValidator()
        {
            RuleFor(x => x.LanguageId)
                .NotNull().WithMessage("Dil ID'si zorunludur");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Blog başlığı zorunludur")
                .MaximumLength(255).WithMessage("Blog başlığı en fazla 255 karakter olabilir")
                .MinimumLength(5).WithMessage("Blog başlığı en az 5 karakter olmalıdır");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Blog içeriği zorunludur")
                .MinimumLength(50).WithMessage("Blog içeriği en az 50 karakter olmalıdır");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Blog slug'ı zorunludur")
                .MaximumLength(255).WithMessage("Blog slug'ı en fazla 255 karakter olabilir")
                .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Geçerli bir slug formatı giriniz (sadece küçük harf, rakam ve tire)");

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
