using FluentValidation;
using DermaKlinik.API.Application.Features.Menus.Queries;

namespace DermaKlinik.API.Application.Features.Menus.Queries.Validators
{
    public class GetMenusByPermissionQueryValidator : AbstractValidator<GetMenusByPermissionQuery>
    {
        public GetMenusByPermissionQueryValidator()
        {
            RuleFor(x => x.Permission)
                .NotEmpty().WithMessage("İzin adı boş olamaz")
                .MaximumLength(100).WithMessage("İzin adı en fazla 100 karakter olabilir");
        }
    }
} 