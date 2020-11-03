using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Persistence;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class PasswordRepository : AsyncRepository<Password>, IPasswordRepository
    {
        public PasswordRepository(CactusPassDbContext db) : base(db)
        {
        }

        public IQueryable<Password> SelectUserPasswords(string userId)
            => SelectAsync().Where(p => p.UserId == userId);
    }
}
