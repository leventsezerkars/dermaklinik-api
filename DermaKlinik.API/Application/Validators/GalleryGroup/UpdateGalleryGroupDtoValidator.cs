using FluentValidation;
using DermaKlinik.API.Application.DTOs.GalleryGroup;

namespace DermaKlinik.API.Application.Validators.GalleryGroup
{
    public class UpdateGalleryGroupDtoValidator : AbstractValidator<UpdateGalleryGroupDto>
    {
        public UpdateGalleryGroupDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Grup adı boş olamaz")
                .MaximumLength(255).WithMessage("Grup adı 255 karakterden uzun olamaz");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Açıklama 1000 karakterden uzun olamaz");

            RuleFor(x => x.IsDeletable)
                .NotNull().WithMessage("Silinebilirlik durumu belirtilmelidir");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Aktiflik durumu belirtilmelidir");
        }
    }
}
