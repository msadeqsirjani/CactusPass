using Application.Extensions;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.IoC.Extensions
{
    public static class JwtExtensions
    {
        public static IServiceCollection RegisterJwtService(this IServiceCollection services, IConfiguration config)
        {
            var issuer = config["Jwt:Issuer"];
            var key = config["Jwt:Key"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var token = context.HttpContext.GetAuthenticationToken();

                            if (!string.IsNullOrEmpty(token))
                            {
                                var jwtService =
                                    context.HttpContext.RequestServices.GetRequiredService<IJwtService>();

                                if (await jwtService.IsExistTokenAsync(token))
                                {
                                    context.Success();
                                    return;
                                }
                            }

                            context.Fail("توکن وارد شده اعتبار ندارد");
                        }
                    };
                });

            return services;
        }
    }
}
