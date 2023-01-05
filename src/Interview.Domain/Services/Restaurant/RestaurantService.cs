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

    public IQueryable<Domain.Restaurant.Restaurant> OdataResturants()
    {
        var query = _restaurantRepository.OdataResturants();
        return query;
    }

    public IQueryable<RestaurantData> OdataResturantsWithViewModel()
    {
        var query = _restaurantRepository.OdataResturantsWithViewModel();
        return query;
    }

    public IQueryable<RestaurantView> OdataResturantsWithView()
    {
        var query = _restaurantRepository.OdataResturantsWithView();
        return query;
    }

    public IQueryable<RestaurantView1> OdataResturantsWithAutomapper()
    {
        var query = _restaurantRepository.OdataResturantsWithAutomapper();
        return query;
    }
}