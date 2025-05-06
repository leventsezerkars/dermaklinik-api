using FluentValidation;
using DermaKlinik.API.Application.Features.Patients.Commands;

namespace DermaKlinik.API.Application.Features.Patients.Commands.Validators
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş olamaz")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz")
                .MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz")
                .Matches(@"^[0-9]{10}$").WithMessage("Geçerli bir telefon numarası giriniz (10 haneli)");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Doğum tarihi boş olamaz")
                .LessThan(DateTime.Now).WithMessage("Doğum tarihi bugünden büyük olamaz");

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Adres en fazla 200 karakter olabilir");
        }
    }
} 