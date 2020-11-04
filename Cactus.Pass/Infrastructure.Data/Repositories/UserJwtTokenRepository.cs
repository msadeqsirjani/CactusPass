using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Persistence;

namespace Infrastructure.Data.Repositories
{
    public class UserJwtTokenRepository : AsyncRepository<UserJwtToken>, IUserJwtTokenRepository
    {
        public UserJwtTokenRepository(CactusPassDbContext db) : base(db)
        {
        }
    }
}
