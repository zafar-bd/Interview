namespace Interview.Domain.Services.Restaurant;
public interface IRestaurantService
{
    Task<RestaurantScheduleViewModel> GetRestaurantSchedulesAsync(RestaurantQueryDto restaurantQueryDto, CancellationToken cancellationToken);
    RestaurantScheduleViewModel GetRestaurantSchedules(RestaurantQueryDto restaurantQueryDto, CancellationToken cancellationToken);
}
