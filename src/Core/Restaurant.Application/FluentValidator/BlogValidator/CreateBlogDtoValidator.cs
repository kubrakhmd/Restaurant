using FluentValidation;
using Restaurant.Application.DTOs;


namespace Restaurant.Application.FluentValidator.BlogValidator
{
    public class CreateBlogDtoValidator:AbstractValidator<CreateBlogDto>
    {
        public CreateBlogDtoValidator()
        {
            RuleFor(b=>b.Title).NotEmpty().WithMessage("Title Required")
                .MaximumLength(100).WithMessage("Must Contains max 100 symbols")
                .Matches(@"^[A-Za-z\s0-9]*$");
            RuleFor(b => b.Article).NotEmpty().WithMessage("Article Required")
              .MaximumLength(100).WithMessage("Must Contains max 100 symbols")
              .Matches(@"^[A-Za-z\s0-9]*$");
            RuleFor(p => p.TagIds)
                .Must(c => c.Count > 0);
        }
    }
}
