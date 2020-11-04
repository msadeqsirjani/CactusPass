using Application.Extensions;
using Application.Interfaces;
using Application.ViewModel.Note;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    public class NoteController : BaseController
    {
        private readonly INoteService _noteService;
        private readonly IJwtService _jwtService;

        private readonly IValidator<NoteAddDto> _noteAddDtoValidator;

        public NoteController(INoteService noteService, IJwtService jwtService, IValidator<NoteAddDto> noteAddDtoValidator)
        {
            _noteService = noteService;
            _jwtService = jwtService;
            _noteAddDtoValidator = noteAddDtoValidator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddMessage(NoteAddDto messageAddDto)
        {
            var validationResult = await _noteAddDtoValidator.ValidateAsync(messageAddDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(new { errorMessage = "در اعتبارسنجی مشکلی پیش آمده است" });
            }

            var token = HttpContext.GetAuthenticationToken();
            var userJwtToken = await _jwtService.GetJwtTokenAsync(token);

            var messageGetDto = await _noteService.AddAsync(userJwtToken.UserId, messageAddDto);

            return Ok(new { messageObj = messageGetDto, message = "پیام با موفقیت اضافه شد" });
        }
    }
}
