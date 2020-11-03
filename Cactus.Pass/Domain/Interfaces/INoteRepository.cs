using Domain.Entities;
using System.Linq;

namespace Domain.Interfaces
{
    public interface INoteRepository : IAsyncRepository<Note>
    {
        IQueryable<Note> SelectUserNotesAsync(string userId);
    }
}
