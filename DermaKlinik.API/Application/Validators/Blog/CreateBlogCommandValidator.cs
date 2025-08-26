using DermaKlinik.API.Application.Features.Blog.Commands;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.Blog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator()
        {
            RuleFor(x => x.CreateBlogDto)
                .NotNull().WithMessage("Blog verisi zorunludur");

            When(x => x.CreateBlogDto != null, () =>
            {
                RuleFor(x => x.CreateBlogDto)
                    .SetValidator(new CreateBlogDtoValidator());
            });
        }
    }
}
