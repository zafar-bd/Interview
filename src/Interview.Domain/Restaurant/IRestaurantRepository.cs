using Interview.Domain.ViewModel;

namespace Interview.Domain.Restaurant;

public interface IRestaurantRepository : IAsyncRepository<Restaurant>
{
    Task<RestaurantScheduleViewModel> GetRestaurantSchedulesAsync(RestaurantQueryDto restaurantQueryDto, CancellationToken cancellationToken);
    RestaurantScheduleViewModel GetRestaurantSchedules(RestaurantQueryDto restaurantQueryDto, CancellationToken cancellationToken);
    IQueryable<Restaurant> OdataResturants();
}