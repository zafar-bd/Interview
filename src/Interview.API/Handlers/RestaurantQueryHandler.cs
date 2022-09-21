namespace Interview.API.Handlers;

public class RestaurantQueryHandler : IRequestHandler<RestaurantQueryDto, RestaurantScheduleViewModel>
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantQueryHandler(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    public Task<RestaurantScheduleViewModel> Handle(RestaurantQueryDto request, CancellationToken cancellationToken)
    {
        return _restaurantService.GetRestaurantSchedulesAsync(request, cancellationToken);
    }
}
