using DotNetCourseNew.Entities;
using FluentValidation;

namespace DotNetCourseNew.Models.Validators
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var isEmailUsed = dbContext.Users.Any(u => u.Email == value);
                    if (isEmailUsed) context.AddFailure("Email", "Email is taken");
                });

            RuleFor(x => x.Password)
                .MinimumLength(5);

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}