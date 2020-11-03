using Application.Extensions;
using Application.Interfaces;
using Application.ViewModel.Account;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;

        public AccountController(IJwtService jwtService, IUserService userService, IValidator<RegisterDto> registerValidator, IValidator<LoginDto> loginValidator)
        {
            _jwtService = jwtService;
            _userService = userService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var validationResult = await _registerValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return BadRequest();
            }

            var identityResult = await _userService.SignUpAsync(model);

            if (identityResult.Succeeded)
            {
                return Ok(new { message = "حساب کاربری شما با موفقیت ایجاد شد!" });
            }

            var error = new { errorMessage = identityResult.Errors.First().Description };

            return BadRequest(error);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            var validationResult = await _loginValidator.ValidateAsync(userDto);

            if (!validationResult.IsValid)
            {
                return BadRequest();
            }

            var signInResult = await _userService.SignInAsync(userDto.Username, userDto.Password);

            var error = new { errorMessage = "نام کاربری یا رمز عبور وارد شده اشتباه می باشد" };

            if (signInResult == null) return BadRequest(error);

            if (signInResult.Succeeded)
            {
                var userJwtToken = await _jwtService.GenerateAndSaveJwtTokenAsync(userDto);

                return Ok(new
                {
                    message = "ورود به حساب کاربری با موفقیت انجام شد!",
                    token = userJwtToken.AccessToken
                });
            }

            if (signInResult.IsLockedOut)
            {
                error = new { errorMessage = "حساب کاربری شما به دلیل 5 بار ورود ناموفق به مدت 5 دقیقه قفل شده است" };
            }

            return BadRequest(error);
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            var token = HttpContext.GetAuthenticationToken();

            await _jwtService.DeleteJwtTokenAsync(token);
            return Ok(new { message = "با موفقیت از حساب کاربری خود خارج شدید" });
        }
    }
}
