using DermaKlinik.API.Application.DTOs.GalleryGroup;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.Gallery
{
    public class CreateGalleryGroupDtoValidator : AbstractValidator<CreateGalleryGroupDto>
    {
        public CreateGalleryGroupDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Grup adı zorunludur")
                .MaximumLength(255).WithMessage("Grup adı en fazla 255 karakter olabilir")
                .MinimumLength(2).WithMessage("Grup adı en az 2 karakter olmalıdır");
        }
    }
}
