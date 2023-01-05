using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Interview.API.Controllers
{
    [Route("api/o-resturants")]
    [ApiController]
    public class ResturantOdataController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public ResturantOdataController(IRestaurantService restaurantService)
        {
            this._restaurantService = restaurantService;
        }

        
        [HttpGet,EnableQuery]
        public ActionResult<IQueryable<Restaurant>> GetAllResturants()
        {
            IQueryable<Restaurant> retrievedRes = this._restaurantService.OdataResturants();
            return Ok(retrievedRes);
        }

        [HttpGet("view"), EnableQuery]
        public ActionResult<IQueryable<RestaurantView>> GetAllResturantsWithView()
        {
            IQueryable<RestaurantView> retrievedRes = this._restaurantService.OdataResturantsWithView();
            return Ok(retrievedRes);
        }

        [HttpGet("view-model"), EnableQuery]
        public ActionResult<IQueryable<RestaurantData>> GetAllResturantsWithViewModel()
        {
            IQueryable<RestaurantData> retrievedRes = this._restaurantService.OdataResturantsWithViewModel();
            return Ok(retrievedRes);
        }

        [HttpGet("automapper"), EnableQuery]
        public ActionResult<IQueryable<RestaurantView1>> GetAllResturantsWithAutomapper()
        {
            IQueryable<RestaurantView1> retrievedRes = this._restaurantService.OdataResturantsWithAutomapper();
            return Ok(retrievedRes);
        }
    }
}
