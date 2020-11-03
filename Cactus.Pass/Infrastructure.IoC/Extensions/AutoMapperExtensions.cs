using Application.Mapper.Profiles;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection RegisterAutoMapperService(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new PasswordProfile());
                c.AddProfile(new NoteProfile());
            });

            var mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
