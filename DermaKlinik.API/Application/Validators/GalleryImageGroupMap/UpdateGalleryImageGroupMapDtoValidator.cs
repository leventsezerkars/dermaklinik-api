using FluentValidation;
using DermaKlinik.API.Application.DTOs.GalleryImageGroupMap;

namespace DermaKlinik.API.Application.Validators.GalleryImageGroupMap
{
    public class UpdateGalleryImageGroupMapDtoValidator : AbstractValidator<UpdateGalleryImageGroupMapDto>
    {
        public UpdateGalleryImageGroupMapDtoValidator()
        {
            RuleFor(x => x.ImageId)
                .NotEmpty().WithMessage("Resim ID'si boş olamaz");

            RuleFor(x => x.GroupId)
                .NotEmpty().WithMessage("Grup ID'si boş olamaz");

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Sıralama 0'dan küçük olamaz");
        }
    }
}
