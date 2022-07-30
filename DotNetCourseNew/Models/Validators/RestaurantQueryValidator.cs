using DotNetCourseNew.Entities;
using FluentValidation;

namespace DotNetCourseNew.Models.Validators;

public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
{
    private int[] allowedPageSizes = new[] { 5, 10, 15 };

    private string[] allowedSortByColumnNames = new[]
    {
        nameof(Restaurant.Name),
        nameof(Restaurant.Description),
        nameof(Restaurant.Category)
    };

    public RestaurantQueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(r => r.PageSize).Custom((value, context) =>
        {
            if (!allowedPageSizes.Contains(value))
            {
                context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPageSizes)}]");
            }
        });
        RuleFor(r => r.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
            .WithMessage($"SortBy is optional or must be in [{string.Join(",", allowedSortByColumnNames)}]");
    }
}