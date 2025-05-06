using FluentValidation;
using DermaKlinik.API.Application.Features.Menus.Commands;

namespace DermaKlinik.API.Application.Features.Menus.Commands.Validators
{
    public class DeleteMenuCommandValidator : AbstractValidator<DeleteMenuCommand>
    {
        public DeleteMenuCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Menü ID'si boş olamaz.");
        }
    }
} 