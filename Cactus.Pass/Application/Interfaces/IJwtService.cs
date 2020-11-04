using Application.ViewModel.Account;
using Application.ViewModel.UserJwtToken;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJwtService
    {
        UserJwtTokenDto GenerateJwtToken(LoginDto userDto);

        Task<UserJwtToken> SaveJwtTokenAsync(UserJwtTokenDto userJwtTokenDto);

        Task<UserJwtTokenDto> GenerateAndSaveJwtTokenAsync(LoginDto userDto);

        Task<string> HashToken(string token);

        Task<bool> IsExistTokenAsync(string token);

        Task<UserJwtToken> GetJwtTokenAsync(string token);

        Task DeleteJwtTokenAsync(UserJwtToken userJwtToken);

        Task DeleteJwtTokenAsync(string token);
    }
}
