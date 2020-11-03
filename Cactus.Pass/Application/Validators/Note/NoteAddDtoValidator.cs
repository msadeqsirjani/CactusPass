using Application.Validators.Common;
using Application.ViewModel.Note;
using FluentValidation;

namespace Application.Validators.Note
{
    public class NoteAddDtoValidator : EntityAddDtoValidator<NoteAddDto>
    {
        public NoteAddDtoValidator()
        {
            RuleFor(n => n.Title)
                .NotNull()
                .WithMessage("عنوان یاک نمی تواند خالی باشد.")
                .MaximumLength(256)
                .WithMessage("عنوان نمی تواند بیشتر از 256 کاراکتر باشد.");

            RuleFor(n => n.Body)
                .NotNull()
                .WithMessage("بدنه پیام نمی تواند خالی باشد.")
                .MaximumLength(2048)
                .WithMessage("بدنه پیام نمی تواند بیشتز از 2048 کاراکتر باشد.");
        }
    }
}
