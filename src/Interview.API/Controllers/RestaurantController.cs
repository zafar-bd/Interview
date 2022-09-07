namespace Interview.API.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetRestaurants([FromQuery] RestaurantQueryDto args, CancellationToken cancellationToken)
        => Ok(await base.Mediator.Send(args, cancellationToken));
    }
}
