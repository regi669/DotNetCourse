using FluentValidation;

namespace DotNetCourseNew.Models.Validators;

public class LoginUserDTOValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDTOValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
        
        RuleFor(x => x.Password)
            .MinimumLength(5);
    }
}