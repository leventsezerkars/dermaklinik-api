using DermaKlinik.API.Application.DTOs.BlogCategory;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.BlogCategory
{
    public class CreateBlogCategoryDtoValidator : AbstractValidator<CreateBlogCategoryDto>
    {
        public CreateBlogCategoryDtoValidator()
        {
            RuleFor(x => x.Translations)
                .NotEmpty().WithMessage("En az bir dil için çeviri zorunludur")
                .Must(translations => translations != null && translations.Any())
                .WithMessage("En az bir dil için çeviri zorunludur");

            RuleForEach(x => x.Translations)
                .SetValidator(new CreateBlogCategoryTranslationDtoValidator());
        }
    }
}
