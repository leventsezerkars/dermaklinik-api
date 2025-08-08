using DermaKlinik.API.Application.DTOs.CompanyInfo;
using FluentValidation;

namespace DermaKlinik.API.Application.Validators.CompanyInfo
{
    public class UpdateCompanyInfoDtoValidator : AbstractValidator<UpdateCompanyInfoDto>
    {
        public UpdateCompanyInfoDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID zorunludur");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Şirket adı zorunludur")
                .MaximumLength(255).WithMessage("Şirket adı en fazla 255 karakter olabilir")
                .MinimumLength(2).WithMessage("Şirket adı en az 2 karakter olmalıdır");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres zorunludur")
                .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir")
                .MinimumLength(10).WithMessage("Adres en az 10 karakter olmalıdır");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon numarası zorunludur")
                .MaximumLength(100).WithMessage("Telefon numarası en fazla 100 karakter olabilir")
                .Matches(@"^[\+]?[0-9\s\-\(\)]+$").WithMessage("Geçerli bir telefon numarası giriniz");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi zorunludur")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz")
                .MaximumLength(255).WithMessage("E-posta adresi en fazla 255 karakter olabilir");

            RuleFor(x => x.LogoUrl)
                .NotEmpty().WithMessage("Logo zorunludur");

            RuleFor(x => x.Facebook)
                .MaximumLength(255).WithMessage("Facebook URL'si en fazla 255 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.Facebook));

            RuleFor(x => x.Twitter)
                .MaximumLength(255).WithMessage("Twitter URL'si en fazla 255 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.Twitter));

            RuleFor(x => x.Instagram)
                .MaximumLength(255).WithMessage("Instagram URL'si en fazla 255 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.Instagram));

            RuleFor(x => x.LinkedIn)
                .MaximumLength(255).WithMessage("LinkedIn URL'si en fazla 255 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.LinkedIn));

            RuleFor(x => x.GoogleMapsUrl)
                .MaximumLength(1000).WithMessage("Google Maps URL'si en fazla 1000 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.GoogleMapsUrl));

            RuleFor(x => x.GoogleAnalyticsCode)
                .MaximumLength(1000).WithMessage("Google Analytics kodu en fazla 1000 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.GoogleAnalyticsCode));

            RuleFor(x => x.MetaTitle)
                .MaximumLength(255).WithMessage("Meta başlık en fazla 255 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.MetaTitle));

            RuleFor(x => x.MetaDescription)
                .MaximumLength(1000).WithMessage("Meta açıklama en fazla 1000 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.MetaDescription));

            RuleFor(x => x.MetaKeywords)
                .MaximumLength(255).WithMessage("Meta anahtar kelimeler en fazla 255 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.MetaKeywords));

            RuleFor(x => x.WorkingHours)
                .MaximumLength(255).WithMessage("Çalışma saatleri en fazla 255 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.WorkingHours));
        }
    }
} 