

using FluentValidation;
using Restaurant.Application.DTOs;

namespace Restaurant.Application.FluentValidator
{
    public class CreateTagDtoValidator:AbstractValidator<CreateTagDto>
    {
        public CreateTagDtoValidator()
        {
            RuleFor(t=>t.Name).NotEmpty()
                .WithMessage("Name Required")
                .MaximumLength(100)
                    .WithMessage("Must contains max 100 symbols")
                .Matches(@"^[A-Za-z]*$");
        }
    }
}
