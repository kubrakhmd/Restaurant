using FluentValidation;
using Restaurant.Application.DTOs;


namespace Restaurant.Application.FluentValidator.AuthorDtoValidator
{
   public class CreateAuthorDtoValidator:AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorDtoValidator()
        {
            RuleFor(a=>a.Name).NotEmpty().WithMessage("Name Required")
                .MaximumLength(25).WithMessage("Name must contains max 25 symbols")
                .Matches(@"^[A-Za-z]*$");
            RuleFor(a => a.Surname).NotEmpty().WithMessage("Surname Required")
               .MaximumLength(30).WithMessage("Surname must contains max 30 symbols")
               .Matches(@"^[A-Za-z]*$");
        }
    }
}
