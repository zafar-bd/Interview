namespace Interview.API.Controllers;

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
    public async Task<IActionResult> GetRestaurants([FromQuery] RestaurantQueryDto request, CancellationToken cancellationToken)
    {
        request.CacheKey = $"{request.Name}_{request.RestaurantId}_{request.DayId}_{request.Start}_{request.End}_{request.PageMaxSize}_{request.PageIndex}";
        return Ok(await mediator.Send(request, cancellationToken));
    }
}
