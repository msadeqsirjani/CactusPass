using Application.ViewModel.Common;
using FluentValidation;

namespace Application.Validators.Common
{
    public class EntityEditDtoValidator<TEntityEditDto> : AbstractValidator<TEntityEditDto>
        where TEntityEditDto : EntityEditDto
    {
        public EntityEditDtoValidator()
        {

        }
    }
}
