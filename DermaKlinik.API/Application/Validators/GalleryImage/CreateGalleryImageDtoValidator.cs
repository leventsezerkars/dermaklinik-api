using FluentValidation;
using DermaKlinik.API.Application.DTOs.GalleryImage;

namespace DermaKlinik.API.Application.Validators.GalleryImage
{
    public class CreateGalleryImageDtoValidator : AbstractValidator<CreateGalleryImageDto>
    {
        public CreateGalleryImageDtoValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Resim URL'si boş olamaz")
                .MaximumLength(500).WithMessage("Resim URL'si 500 karakterden uzun olamaz");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz")
                .MaximumLength(255).WithMessage("Başlık 255 karakterden uzun olamaz");

            RuleFor(x => x.AltText)
                .MaximumLength(255).WithMessage("Alt metin 255 karakterden uzun olamaz");

            RuleFor(x => x.Caption)
                .MaximumLength(255).WithMessage("Açıklama 255 karakterden uzun olamaz");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Aktiflik durumu belirtilmelidir");
        }
    }
}
