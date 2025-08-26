using DermaKlinik.API.Application.Features.Menu.Commands;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.Menu
{
    public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
            RuleFor(x => x.CreateMenuDto)
                .NotNull().WithMessage("Menü verisi zorunludur");

            When(x => x.CreateMenuDto != null, () =>
            {
                RuleFor(x => x.CreateMenuDto)
                    .SetValidator(new CreateMenuDtoValidator());
            });
        }
    }
}
