using DermaKlinik.API.Application.DTOs.Blog;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.Blog
{
    public class CreateBlogDtoValidator : AbstractValidator<CreateBlogDto>
    {
        public CreateBlogDtoValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Blog kategorisi zorunludur");

            RuleFor(x => x.Translations)
                .NotEmpty().WithMessage("En az bir dil için çeviri zorunludur")
                .Must(translations => translations != null && translations.Any())
                .WithMessage("En az bir dil için çeviri zorunludur");

            RuleForEach(x => x.Translations)
                .SetValidator(new CreateBlogTranslationDtoValidator());
        }
    }
}
