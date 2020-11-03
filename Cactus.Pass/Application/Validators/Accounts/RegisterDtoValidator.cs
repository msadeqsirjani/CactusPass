using Application.ViewModel.Account;
using FluentValidation;

namespace Application.Validators.Accounts
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.Username)
                .MinimumLength(5)
                .WithMessage("نام کاربری نمی تواند کمتر از 5 کاراکتر باشد")
                .MaximumLength(20)
                .WithMessage("نام کاربری نمی تواند بیشتز از 20 کاراکتر باشد");

            RuleFor(r => r.EmailAddress)
                .MinimumLength(3)
                .WithMessage("آدرس ایمیل نمی تواند کمتر از 3 کاراکتر باشد.")
                .MaximumLength(150)
                .WithMessage("آدرس ایمیل نمی تواند ییشتر از 150 گاراکتر باشد.")
                .EmailAddress()
                .WithMessage("آدرس ایمیل به درستی وارد نشده است.");

            RuleFor(r => r.Password)
                .MinimumLength(6)
                .WithMessage("رمز عبور نمی تواند کمتر از 6 کاراکتر باشد")
                .MaximumLength(50)
                .WithMessage("رمز غبور نمی تواند بیشتر از 50 کاراکتر باشد.");
        }
    }
}
