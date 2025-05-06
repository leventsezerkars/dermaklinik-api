using FluentValidation;
using DermaKlinik.API.Application.Features.Menus.Commands;

namespace DermaKlinik.API.Application.Features.Menus.Commands.Validators
{
    public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Menü adı boş olamaz")
                .MaximumLength(100).WithMessage("Menü adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Açıklama en fazla 200 karakter olabilir");

            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("İkon boş olamaz")
                .MaximumLength(50).WithMessage("İkon en fazla 50 karakter olabilir");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL boş olamaz")
                .MaximumLength(200).WithMessage("URL en fazla 200 karakter olabilir");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Sıralama 0'dan küçük olamaz");

            RuleFor(x => x.RequiredPermission)
                .MaximumLength(100).WithMessage("İzin adı en fazla 100 karakter olabilir");
        }
    }
} 