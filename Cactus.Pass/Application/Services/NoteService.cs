using Application.Interfaces;
using Application.ViewModel.Note;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NoteService : AsyncService
        <Note, NoteAddDto, NoteEditDto, NoteGetDto>,
        INoteService
    {
        protected readonly INoteRepository NoteRepository;

        public NoteService(INoteRepository repository, IMapper mapper) : base(repository, mapper)
        {
            NoteRepository = repository;
        }

        public async Task<List<NoteGetDto>> GetNotesAsync(string userId)
            => await NoteRepository.SelectUserNotesAsync(userId)
                .Select(n => MapEntityToGetDto(n))
                .ToListAsync();

        public async Task<bool> ExistsAsync(string userId, string noteId)
            => await NoteRepository.ExistsAsync
                (n => n.Id == noteId && n.UserId == userId);

        public async Task<NoteGetDto> AddAsync(string userId, NoteAddDto noteAddDto)
        {
            var entity = MapAddDtoToEntity(noteAddDto);

            entity.UserId = userId;

            return await AddAsync(entity);
        }

        public async Task<NoteGetDto> UpdateAsync(string userId, string noteId, NoteEditDto noteEditDto)
        {
            var entity = MapEditDtoToEntity(noteEditDto);

            entity.UserId = userId;

            return await UpdateAsync(noteId, entity);
        }
    }
}
