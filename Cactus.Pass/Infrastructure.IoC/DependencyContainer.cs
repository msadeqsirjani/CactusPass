using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Persistence;
using Infrastructure.Data.Repositories;
using Infrastructure.IoC.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config, IMvcBuilder builder)
        {
            services.AddDbContextPool<CactusPassDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("CactusPassDbConnection")));

            services.RegisterIdentityService();

            services.RegisterJwtService(config);

            services.RegisterSwaggerService();

            services.RegisterFluentValidationService(builder);

            services.RegisterAutoMapperService();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserJwtTokenRepository, UserJwtTokenRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IJwtService, JwtService>();

            services.AddTransient<IPasswordRepository, PasswordRepository>();
            services.AddTransient<IPasswordService, PasswordService>();

            services.AddTransient<INoteRepository, NoteRepository>();
            services.AddTransient<INoteService, NoteService>();

            return services;
        }

        public static IApplicationBuilder EnableMiddleware(this IApplicationBuilder app)
        {
            app.EnableSwaggerMiddleware();

            return app;
        }
    }
}
