namespace Interview.Domain.Restaurant;

public interface IRestaurantRepository : IAsyncRepository<Restaurant>
{
    Task<RestaurantScheduleViewModel> GetRestaurantSchedulesAsync(RestaurantQueryDto restaurantQueryDto, CancellationToken cancellationToken);
}