using Domain.Entities;
using System.Linq;

namespace Domain.Interfaces
{
    public interface IPasswordRepository : IAsyncRepository<Password>
    {
        IQueryable<Password> SelectByUser(string userId);
    }
}
