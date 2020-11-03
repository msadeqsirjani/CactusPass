using Application.Mapper.Resolver;
using Application.ViewModel.Password;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Profiles
{
    public class PasswordProfile : Profile
    {
        public PasswordProfile()
        {
            CreateMap<Password, PasswordGetDto>()
                .ForMember(d => d.Password,
                    o => o.MapFrom<DecryptPasswordResolver<Password, PasswordGetDto>>())
                .ForMember(d => d.CreatePersianDateTime,
                    o => o.MapFrom<CreatePersianDateTimeResolver<Password, PasswordGetDto>>())
                .ForMember(d => d.UpdatePersianDateTime,
                    o => o.MapFrom<UpdatePersianDateTimeResolver<Password, PasswordGetDto>>());

            CreateMap<Password, PasswordEditDto>()
                .ForMember(d => d.Password,
                    o => o.MapFrom<DecryptPasswordResolver<Password, PasswordEditDto>>());

            CreateMap<PasswordEditDto, Password>()
                .ForMember(d => d.HashedPassword,
                    o => o.MapFrom<EncryptPasswordResolver<PasswordEditDto, Password>>());

            CreateMap<PasswordAddDto, Password>()
                .ForMember(d => d.HashedPassword,
                    o => o.MapFrom<EncryptPasswordResolver<PasswordAddDto, Password>>());
        }
    }
}
