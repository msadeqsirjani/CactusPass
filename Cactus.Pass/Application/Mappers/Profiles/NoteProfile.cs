using Application.Mapper.Resolver;
using Application.ViewModel.Note;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Profiles
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteGetDto>()
                .ForMember(d => d.CreatePersianDateTime,
                    o => o.MapFrom<CreatePersianDateTimeResolver<Note, NoteGetDto>>())
                .ForMember(d => d.UpdatePersianDateTime,
                    o => o.MapFrom<UpdatePersianDateTimeResolver<Note, NoteGetDto>>());

            CreateMap<Note, NoteEditDto>();

            CreateMap<NoteEditDto, Note>();

            CreateMap<NoteAddDto, Note>();
        }
    }
}
