using FluentValidation;

namespace GGBlog.Models.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator() 
        { 
            RuleFor(x => x.Mail).NotNull().WithMessage("Kullanıcı adı boş olamaz!").NotEmpty().WithMessage("Kullanıcı adı boş olamaz!");
            RuleFor(x => x.Kullanici_sifre).NotNull().WithMessage("Şifre boş olamaz!").NotEmpty().WithMessage("Şifre boş  boş olamaz!");
            RuleFor(x => x.Kullanici_sifre).MaximumLength(32).WithMessage("Şifre 32 karakterden fazla olamaz!");
        }
    }
}
