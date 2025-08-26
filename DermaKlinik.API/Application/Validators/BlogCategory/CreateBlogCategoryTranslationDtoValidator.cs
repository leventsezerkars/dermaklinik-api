using DermaKlinik.API.Application.DTOs.BlogCategory;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.BlogCategory
{
    public class CreateBlogCategoryTranslationDtoValidator : AbstractValidator<CreateBlogCategoryTranslationDto>
    {
        public CreateBlogCategoryTranslationDtoValidator()
        {
            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage("Dil ID'si zorunludur");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı zorunludur")
                .MaximumLength(255).WithMessage("Kategori adı en fazla 255 karakter olabilir")
                .MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır");
        }
    }
}
