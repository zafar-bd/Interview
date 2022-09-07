namespace Interview.API.Mediator
{
    public class RestaurantQueryHandler : IRequestHandler<RestaurantQueryDto, RestaurantScheduleViewModel>
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantQueryHandler(IRestaurantService restaurantService)
        {
            this._restaurantService = restaurantService;
        }

        public Task<RestaurantScheduleViewModel> Handle(RestaurantQueryDto request, CancellationToken cancellationToken)
        {
            return _restaurantService.GetRestaurantSchedulesAsync(request, cancellationToken);
        }
    }
}
