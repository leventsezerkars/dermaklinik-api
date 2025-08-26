using DermaKlinik.API.Application.DTOs.GalleryImage;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.Gallery
{
    public class CreateGalleryImageDtoValidator : AbstractValidator<CreateGalleryImageDto>
    {
        public CreateGalleryImageDtoValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Resim URL'si zorunludur")
                .MaximumLength(500).WithMessage("Resim URL'si en fazla 500 karakter olabilir");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Resim başlığı zorunludur")
                .MaximumLength(255).WithMessage("Resim başlığı en fazla 255 karakter olabilir")
                .MinimumLength(2).WithMessage("Resim başlığı en az 2 karakter olmalıdır");

            RuleFor(x => x.AltText)
                .MaximumLength(255).WithMessage("Alt metin en fazla 255 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.AltText));

            RuleFor(x => x.Caption)
                .MaximumLength(255).WithMessage("Resim açıklaması en fazla 255 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.Caption));
        }
    }
}
