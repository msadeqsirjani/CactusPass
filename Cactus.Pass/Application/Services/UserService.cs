using Application.Interfaces;
using Application.ViewModel.Account;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository AppUserRepository;
        protected readonly SignInManager<AppUser> SignInManager;
        protected readonly UserManager<AppUser> UserManager;

        public UserService(IUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            AppUserRepository = appUserRepository;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        public virtual async Task<AppUser> GetUserAsync(string username)
            => await AppUserRepository.FirstOrDefaultAsync(u => u.NormalizedUserName == username.ToUpper());

        public virtual async Task<string> GetUserIdAsync(string username)
            => (await GetUserAsync(username)).Id;

        public virtual async Task<IdentityResult> SignUpAsync(RegisterDto userDto)
        {
            var user = new AppUser
            {
                UserName = userDto.Username,
                Email = userDto.EmailAddress,
                EmailConfirmed = true
            };

            var identityResult = await UserManager.CreateAsync(user, userDto.Password);

            return identityResult;
        }

        public virtual async Task<SignInResult> SignInAsync(string username, string password, bool lookoutOnFailure = true)
        {
            var user = await GetUserAsync(username);

            if (user == null) return null;

            var signInResult = await SignInManager.CheckPasswordSignInAsync(user, password, lookoutOnFailure);

            return signInResult;
        }
    }
}
