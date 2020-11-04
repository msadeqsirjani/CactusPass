using Application.Extensions;
using Application.ViewModel.Common;
using AutoMapper;
using Domain.Common;

namespace Application.Mapper.Resolver
{
    public class EncryptPasswordResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, string>
        where TSource : IPasswordProperty
        where TDestination : IPasswordHashProperty
    {
        public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
            => source.Password.Encrypt();
    }
}
