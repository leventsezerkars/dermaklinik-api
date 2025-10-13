using FluentValidation;
using DermaKlinik.API.Application.DTOs.GalleryImage;

namespace DermaKlinik.API.Application.Validators.GalleryImage
{
    public class UpdateGalleryImageDtoValidator : AbstractValidator<UpdateGalleryImageDto>
    {
        public UpdateGalleryImageDtoValidator()
        {
            RuleFor(x => x.ImageFile)
                .Must(BeValidImageFile).WithMessage("Geçersiz resim dosyası formatı")
                .Must(BeValidFileSize).WithMessage("Dosya boyutu 10MB'dan büyük olamaz")
                .Must(BeValidImageSize).WithMessage("Resim webde gösterilemeyecek kadar büyük boyutta")
                .When(x => x.ImageFile != null);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz")
                .MaximumLength(255).WithMessage("Başlık 255 karakterden uzun olamaz");

            RuleFor(x => x.TitleEn)
                .MaximumLength(255).WithMessage("İngilizce başlık 255 karakterden uzun olamaz");

            RuleFor(x => x.AltText)
                .MaximumLength(255).WithMessage("Alt metin 255 karakterden uzun olamaz");

            RuleFor(x => x.Caption)
                .MaximumLength(255).WithMessage("Açıklama 255 karakterden uzun olamaz");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Aktiflik durumu belirtilmelidir");

            RuleFor(x => x.GroupIds)
                .NotNull().WithMessage("Grup ID'leri boş olamaz")
                .Must(BeValidGuids).WithMessage("Geçersiz GUID formatı");
        }

        private bool BeValidImageFile(IFormFile? file)
        {
            if (file == null) return true; // Opsiyonel alan
            
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(extension);
        }

        private bool BeValidFileSize(IFormFile? file)
        {
            if (file == null) return true; // Opsiyonel alan
            return file.Length <= 10 * 1024 * 1024; // 10MB
        }

        private bool BeValidImageSize(IFormFile? file)
        {
            if (file == null) return true;
            
            // Çok büyük resimleri kontrol et (50MB'dan büyük)
            var maxSizeForWeb = 50 * 1024 * 1024; // 50MB
            return file.Length <= maxSizeForWeb;
        }

        private bool BeValidGuids(List<Guid> groupIds)
        {
            if (groupIds == null) return false;
            return groupIds.All(guid => guid != Guid.Empty);
        }
    }
}
