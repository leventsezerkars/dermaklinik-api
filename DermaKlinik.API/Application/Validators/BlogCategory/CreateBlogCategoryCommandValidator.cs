using DermaKlinik.API.Application.Features.BlogCategory.Commands;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.BlogCategory
{
    public class CreateBlogCategoryCommandValidator : AbstractValidator<CreateBlogCategoryCommand>
    {
        public CreateBlogCategoryCommandValidator()
        {
            RuleFor(x => x.CreateBlogCategoryDto)
                .NotNull().WithMessage("Blog kategori verisi zorunludur");

            When(x => x.CreateBlogCategoryDto != null, () =>
            {
                RuleFor(x => x.CreateBlogCategoryDto)
                    .SetValidator(new CreateBlogCategoryDtoValidator());
            });
        }
    }
}
