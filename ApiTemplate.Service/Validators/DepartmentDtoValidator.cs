using FluentValidation;

namespace ApiTemplate.DTOs.Validator
{
    public class DepartmentDtoValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentDtoValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Adı daxil edin");
        }
    }
}
