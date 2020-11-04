using Application.Extensions;
using Application.ViewModel.Common;
using AutoMapper;
using Domain.Common;

namespace Application.Mapper.Resolver
{
    public class UpdatePersianDateTimeResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, string>
        where TSource : BaseEntity
        where TDestination : EntityGetDto
    {
        public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
            => source.UpdateDateTime.ToPersianDateTime();
    }
}
