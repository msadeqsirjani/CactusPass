using Domain.Entities.Identity;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> AddAsync(AppUser user);

        Task<AppUser> FirstOrDefaultAsync(Expression<Func<AppUser, bool>> expression);
    }
}
