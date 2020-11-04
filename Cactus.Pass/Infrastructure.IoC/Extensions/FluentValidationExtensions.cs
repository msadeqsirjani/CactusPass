using Application.Validators.Accounts;
using Application.Validators.Note;
using Application.Validators.Password;
using Application.ViewModel.Account;
using Application.ViewModel.Note;
using Application.ViewModel.Password;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection RegisterFluentValidationService(this IServiceCollection services, IMvcBuilder builder)
        {
            builder.AddFluentValidation();

            services.AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>();
            services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();

            services.AddTransient<IValidator<PasswordAddDto>, PasswordAddDtoValidator>();
            services.AddTransient<IValidator<PasswordEditDto>, PasswordEditDtoValidator>();

            services.AddTransient<IValidator<NoteAddDto>, NoteAddDtoValidator>();
            services.AddTransient<IValidator<NoteEditDto>, NoteEditDtoValidator>();

            return services;
        }
    }
}
