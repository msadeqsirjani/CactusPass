using Application.Extensions;
using Application.ViewModel.Common;
using AutoMapper;
using Domain.Common;

namespace Application.Mapper.Resolver
{
    public class CreatePersianDateTimeResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, string>
        where TSource : BaseEntity
        where TDestination : EntityGetDto
    {
        public string Resolve(TSource source, TDestination destination, string destinationMember, ResolutionContext context)
            => source.CreateDateTime.ToPersianDateTime();
    }
}
