using System.Diagnostics;

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
    {
        Debug.WriteLine($"in service statred: {Environment.CurrentManagedThreadId}");
        var res = await _restaurantRepository.GetRestaurantSchedulesAsync(restaurantQueryDto, cancellationToken);

        Debug.WriteLine($"in service end call: {Environment.CurrentManagedThreadId}");

        return res;
    }

    public RestaurantScheduleViewModel GetRestaurantSchedules(RestaurantQueryDto restaurantQueryDto, CancellationToken cancellationToken)
    {
        Debug.WriteLine($"in service statred: {Environment.CurrentManagedThreadId}");
        var res = _restaurantRepository.GetRestaurantSchedules(restaurantQueryDto, cancellationToken);

        Debug.WriteLine($"in service end call: {Environment.CurrentManagedThreadId}");

        return res;
    }

}