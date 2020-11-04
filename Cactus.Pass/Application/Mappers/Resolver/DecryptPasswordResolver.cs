using Application.Extensions;
using Application.ViewModel.Common;
using AutoMapper;
using Domain.Common;

namespace Application.Mapper.Resolver
{
    public class DecryptPasswordResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, string>
        where TSource : IPasswordHashProperty
        where TDestination : IPasswordProperty
    {
        public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
            => source.HashedPassword.Decrypt();
    }
}
