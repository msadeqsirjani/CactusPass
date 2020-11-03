using Domain.Entities.Identity;
using Domain.Interfaces;
using Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly CactusPassDbContext Db;
        protected readonly DbSet<AppUser> DbSet;

        public UserRepository(CactusPassDbContext db)
        {
            Db = db;
            DbSet = Db.Set<AppUser>();
        }

        public virtual async Task<AppUser> AddAsync(AppUser user, bool saveAutomatically = true)
        {
            await DbSet.AddAsync(user);

            if (saveAutomatically)
                await SaveChangesAsync();

            return user;
        }

        public async Task SaveChangesAsync()
        {
            await Db.SaveChangesAsync();
        }

        public virtual async Task<AppUser> FirstOrDefaultAsync(Expression<Func<AppUser, bool>> expression)
            => await DbSet.FirstOrDefaultAsync(expression);
    }
}
