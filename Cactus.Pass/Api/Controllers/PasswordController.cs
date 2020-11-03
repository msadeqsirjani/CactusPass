using Application.Extensions;
using Application.Interfaces;
using Application.ViewModel.Password;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    public class PasswordController : BaseController
    {
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        private readonly IValidator<PasswordAddDto> _passwordAddDtoValidator;
        private readonly IValidator<PasswordEditDto> _passwordEditDtoValidator;

        public PasswordController(IPasswordService passwordService, IJwtService jwtService,
            IValidator<PasswordAddDto> passwordAddDtoValidator, IValidator<PasswordEditDto> passwordEditDtoValidator)
        {
            _passwordService = passwordService;
            _jwtService = jwtService;
            _passwordAddDtoValidator = passwordAddDtoValidator;
            _passwordEditDtoValidator = passwordEditDtoValidator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPasswords()
        {
            var token = HttpContext.GetAuthenticationToken();
            var userJwtToken = await _jwtService.GetJwtTokenAsync(token);

            var passwords = await _passwordService.GetPasswordsAsync(userJwtToken.UserId);

            return Ok(passwords);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetPassword(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { errorMessage = "آیدی نمیتواند خالی باشد" });
            }

            if (!await _passwordService.ExistsAsync(id))
            {
                return BadRequest(new { errorMessage = "رمز عبور مدنظر شما یافت نشد" });
            }

            var password = await _passwordService.SelectAsync(id);

            return Ok(password);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddPassword(PasswordAddDto passwordAddDto)
        {
            var validationResult = await _passwordAddDtoValidator.ValidateAsync(passwordAddDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(new { errorMessage = "اطلاعات به درستی وارد نشده است" });
            }

            var token = HttpContext.GetAuthenticationToken();
            var userJwtToken = await _jwtService.GetJwtTokenAsync(token);

            var passwordGetDto = await _passwordService.AddAsync(userJwtToken.UserId, passwordAddDto);

            return Ok(new { password = passwordGetDto, message = "رمز عبور با موفقیت اضافه شد" });
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> EditPassword(string id, PasswordEditDto passwordEditDto)
        {
            var validationResult = await _passwordEditDtoValidator.ValidateAsync(passwordEditDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(new { errorMessage = "اطلاعات به درستی وارد نشده است" });
            }

            var token = HttpContext.GetAuthenticationToken();
            var userJwtToken = await _jwtService.GetJwtTokenAsync(token);
            var userId = userJwtToken.UserId;

            if (!await _passwordService.ExistsAsync(userId, id))
            {
                return NotFound(new { errorMessage = "رمز عبور مدنظر شما یافت نشد" });
            }

            var passwordGetDto = await _passwordService.UpdateAsync(userId, id, passwordEditDto);

            return Ok(new { password = passwordGetDto, message = "رمز عبور با موفقیت ویرایش شد" });
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeletePassword(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var token = HttpContext.GetAuthenticationToken();
            var userJwtToken = await _jwtService.GetJwtTokenAsync(token);

            if (!await _passwordService.ExistsAsync(userJwtToken.UserId, id))
            {
                return NotFound(new { errorMessage = "رمز عبور مدنظر شما یافت نشد" });
            }

            await _passwordService.DeleteAsync(id);

            return Ok(new { message = "رمز عبور با موفقیت حذف شد" });
        }
    }
}
