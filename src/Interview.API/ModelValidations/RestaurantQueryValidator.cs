namespace Interview.API.ModelValidations;

public class RestaurantQueryValidator : AbstractValidator<RestaurantQueryDto>
{
    public RestaurantQueryValidator()
    {
        RuleFor(r => r.End).NotNull().When(r => r.Start is not null);
        RuleFor(r => r.Start).NotNull().When(r => r.End is not null);
        RuleFor(r => r.PageMaxSize).GreaterThan(0);
        RuleFor(r => r.PageIndex).GreaterThan(0);
        //RuleFor(r => TimeSpan.Parse(r.End)).GreaterThan(r => TimeSpan.Parse(r.Start)).When(r => !string.IsNullOrEmpty(r.Start));
        //RuleFor(r => TimeSpan.Parse(r.Start)).LessThan(r => TimeSpan.Parse(r.End)).When(r => !string.IsNullOrEmpty(r.End));
        RuleFor(r => r.Name).MaximumLength(100);
    }
}
