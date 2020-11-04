using System.Threading.Tasks;
using Application.ViewModel.Account;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<AppUser> GetUserAsync(string username);

        Task<string> GetUserIdAsync(string userName);

        Task<IdentityResult> SignUpAsync(RegisterDto userDto);

        Task<SignInResult> SignInAsync(string username, string password, bool lookoutOnFailure = true);
    }
}