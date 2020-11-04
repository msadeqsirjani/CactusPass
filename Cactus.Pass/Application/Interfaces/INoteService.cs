using Application.ViewModel.Note;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INoteService : IAsyncService
        <Note, NoteAddDto, NoteEditDto, NoteGetDto>
    {
        Task<List<NoteGetDto>> GetNotesAsync(string userId);

        Task<bool> ExistsAsync(string userId, string noteId);

        Task<NoteGetDto> AddAsync(string userId, NoteAddDto noteAddDto);

        Task<NoteGetDto> UpdateAsync(string userId, string noteId, NoteEditDto noteEditDto);
    }
}