using API.DTOs.Users;
using Interview.Domain.Restaurant;
using Interview.Domain.Services;
using Interview.Domain.Services.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
}