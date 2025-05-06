using FluentValidation;
using DermaKlinik.API.Application.Features.Menus.Queries;

namespace DermaKlinik.API.Application.Features.Menus.Queries.Validators
{
    public class GetMenuByIdQueryValidator : AbstractValidator<GetMenuByIdQuery>
    {
        public GetMenuByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Menü ID'si boş olamaz.");
        }
    }
} 