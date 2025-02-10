using FluentValidation;
using Restaurant.Application.DTOs;

namespace Restaurant.Application.FluentValidator.GenreDtoValidator
{
    public class CreateGenreDtoValidator:AbstractValidator<CreateGenreDto>
    {
        public CreateGenreDtoValidator()
        {
            RuleFor(a => a.Name).NotEmpty()
                
                .WithMessage("Name Required")
                .MaximumLength(100)
                    .WithMessage("Must contains max 100 symbols")
                 .Matches(@"^[A-Za-z]*$");
        }
    }
}
