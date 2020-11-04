using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserJwtTokenRepository : IAsyncRepository<UserJwtToken>
    {
    }
}
