namespace Interview.API.Handlers;

public class RestaurantQueryHandler : IRequestHandler<RestaurantQueryDto, RestaurantScheduleViewModel>
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantQueryHandler(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    public async Task<RestaurantScheduleViewModel> Handle(RestaurantQueryDto request, CancellationToken cancellationToken)
    {
        return await _restaurantService.GetRestaurantSchedulesAsync(request, cancellationToken);
    }
}
