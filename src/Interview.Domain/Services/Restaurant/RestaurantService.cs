

namespace API.Services.Users
{
    public class RestaurantService : BaseService, IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(
            IUnitOfWork unitOfWork,
            IRestaurantRepository restaurantRepository) : base(unitOfWork)
        {
            this._restaurantRepository = restaurantRepository;
        }

        public async Task<RestaurantScheduleViewModel> GetRestaurantSchedulesAsync(RestaurantQueryDto restaurantQueryDto, CancellationToken cancellationToken)
        {
            return await _restaurantRepository.GetRestaurantSchedulesAsync(restaurantQueryDto, cancellationToken);
        }
    }
}