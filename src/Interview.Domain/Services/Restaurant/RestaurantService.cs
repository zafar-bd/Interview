namespace Interview.Domain.Services.Restaurant;

public class RestaurantService : BaseService, IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(
        IUnitOfWork unitOfWork,
        IRestaurantRepository restaurantRepository) : base(unitOfWork)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<RestaurantScheduleViewModel> GetRestaurantSchedulesAsync(RestaurantQueryDto restaurantQueryDto, CancellationToken cancellationToken)
    => await _restaurantRepository.GetRestaurantSchedulesAsync(restaurantQueryDto, cancellationToken);
}