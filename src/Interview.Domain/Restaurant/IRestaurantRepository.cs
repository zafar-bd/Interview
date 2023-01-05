using Interview.Domain.ViewModel;

namespace Interview.Domain.Restaurant;

public interface IRestaurantRepository : IAsyncRepository<Restaurant>
{
    Task<RestaurantScheduleViewModel> GetRestaurantSchedulesAsync(RestaurantQueryDto restaurantQueryDto, CancellationToken cancellationToken);
    RestaurantScheduleViewModel GetRestaurantSchedules(RestaurantQueryDto restaurantQueryDto, CancellationToken cancellationToken);
    IQueryable<Restaurant> OdataResturants();
    IQueryable<RestaurantData> OdataResturantsWithViewModel();
    IQueryable<RestaurantView> OdataResturantsWithView();

    IQueryable<RestaurantView1> OdataResturantsWithAutomapper();
}