using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Persistence;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class NoteRepository : AsyncRepository<Note>, INoteRepository
    {
        public NoteRepository(CactusPassDbContext db) : base(db)
        {
        }

        public IQueryable<Note> SelectUserNotesAsync(string userId)
            => SelectAsync().Where(p => p.UserId == userId);
    }
}
