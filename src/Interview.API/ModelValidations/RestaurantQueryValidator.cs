namespace Interview.API.ModelValidations
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQueryDto>
    {
        public RestaurantQueryValidator()
        {
            RuleFor(r => r.End).NotNull().When(r => r.Start is not null);
            RuleFor(r => r.Start).NotNull().When(r => r.End is not null);
            RuleFor(r => r.PageMaxSize).NotNull().When(r => r.PageIndex is not null);
            RuleFor(r => r.PageIndex).NotNull().When(r => r.PageMaxSize is not null);
            RuleFor(r => r.Name).MaximumLength(100);
        }
    }
}
