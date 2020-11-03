using Application.ViewModel.Account;
using FluentValidation;

namespace Application.Validators.Accounts
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.Username)
                .MinimumLength(5)
                .WithMessage("نام کاربری نمی تواند کمتر از 20 کاراکتر باشد.")
                .MaximumLength(20)
                .WithMessage("نام کاربری نمی تواند بیشتر از 20 کاراکتر باشد.");

            RuleFor(l => l.Password)
                .MinimumLength(6)
                .WithMessage("رمز عبور نمی تواند کمتر از 6 کاراکتر باشد")
                .MaximumLength(50)
                .WithMessage("رمز غبور نمی تواند بیشتر از 50 کاراکتر باشد.");
        }
    }
}
