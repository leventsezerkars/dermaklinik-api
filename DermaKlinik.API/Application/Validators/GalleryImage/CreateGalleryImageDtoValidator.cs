using FluentValidation;
using DermaKlinik.API.Application.DTOs.GalleryImage;

namespace DermaKlinik.API.Application.Validators.GalleryImage
{
    public class CreateGalleryImageDtoValidator : AbstractValidator<CreateGalleryImageDto>
    {
        public CreateGalleryImageDtoValidator()
        {
            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("Resim dosyası boş olamaz")
                .Must(BeValidImageFile).WithMessage("Geçersiz resim dosyası formatı")
                .Must(BeValidFileSize).WithMessage("Dosya boyutu 10MB'dan büyük olamaz");

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

        private bool BeValidImageFile(IFormFile file)
        {
            if (file == null) return false;
            
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(extension);
        }

        private bool BeValidFileSize(IFormFile file)
        {
            if (file == null) return false;
            return file.Length <= 10 * 1024 * 1024; // 10MB
        }
    }
}
