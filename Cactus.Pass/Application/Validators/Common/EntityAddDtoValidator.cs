using Application.ViewModel.Common;
using FluentValidation;

namespace Application.Validators.Common
{
    public class EntityAddDtoValidator<TEntityAddDto> : AbstractValidator<TEntityAddDto>
        where TEntityAddDto : EntityAddDto
    {
        public EntityAddDtoValidator()
        {

        }
    }
}
