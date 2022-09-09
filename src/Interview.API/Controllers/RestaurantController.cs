namespace Interview.API.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : BaseApiController
    {
        private readonly IMediator mediator;

        public RestaurantController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetRestaurants([FromQuery] RestaurantQueryDto args, CancellationToken cancellationToken)
        => Ok(await mediator.Send(args, cancellationToken));
    }
}
