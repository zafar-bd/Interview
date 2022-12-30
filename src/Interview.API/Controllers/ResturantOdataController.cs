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
        public ActionResult<IQueryable<Restaurant>> GetAllStudents()
        {
            IQueryable<Restaurant> retrievedStudents = this._restaurantService.OdataResturants();

            return Ok(retrievedStudents);
        }
    }
}
