using Domain.Entities;
using System.Linq;

namespace Domain.Interfaces
{
    public interface IPasswordRepository : IAsyncRepository<Password>
    {
        IQueryable<Password> SelectUserPasswords(string userId);
    }
}
