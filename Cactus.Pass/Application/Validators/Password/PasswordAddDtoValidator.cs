using Application.Validators.Common;
using Application.ViewModel.Password;
using FluentValidation;

namespace Application.Validators.Password
{
    public class PasswordAddDtoValidator : EntityAddDtoValidator<PasswordAddDto>
    {
        public PasswordAddDtoValidator()
        {
            RuleFor(p => p.Username)
                .MaximumLength(20)
                .WithMessage("نام کاربری نمی تواند یبشتزاز 20 گاراکتر باشد.");

            RuleFor(p => p.EmailAddress)
                .MaximumLength(150)
                .WithMessage("آدرس ایمیل نمی تواند بیشتر از 150 کااکتر باشد.")
                .EmailAddress()
                .WithMessage("آدرس ایمیل به درستی وارد نشده است.");

            RuleFor(p => p.Password)
                .NotNull()
                .WithMessage("رمز عبور نمیتواند خالی باشد")
                .NotEmpty()
                .WithMessage("رمز عبور نمیتواند خالی باشد");

            RuleFor(p => p.UsedIn)
                .MaximumLength(1000)
                .WithMessage("مکان های مورد استفاده نمیتواند بیش از 1000 حرف باشد");
        }
    }
}
