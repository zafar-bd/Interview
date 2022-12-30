using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace Interview.API.Controllers;

//[Authorize]
[Route("api/v1.0/restaurants")]
[ApiController]
public class RestaurantController : BaseApiController
{
    private readonly IMediator mediator;
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IMediator mediator, IRestaurantService restaurantService)
    {
        this.mediator = mediator;
        this._restaurantService = restaurantService;
    }

    [HttpGet("async")]
    public async Task<IActionResult> GetRestaurantsWithAsync([FromQuery] RestaurantQueryDto request, CancellationToken cancellationToken)
    {
        Debug.WriteLine($"ctrl action statred: {Environment.CurrentManagedThreadId}");
        request.CacheKey = $"{request.Name}_{request.RestaurantId}_{request.DayId}_{request.Start}_{request.End}_{request.PageMaxSize}_{request.PageIndex}";
        var res = await mediator.Send(request, cancellationToken);

        Debug.WriteLine($"ctrl action data fetched: {Environment.CurrentManagedThreadId}");
       
        return Ok(res);
    }

    [HttpGet("sync")]
    public IActionResult GetRestaurants([FromQuery] RestaurantQueryDto request, CancellationToken cancellationToken)
    {
        Debug.WriteLine($"ctrl action statred: {Environment.CurrentManagedThreadId}");
        var res = _restaurantService.GetRestaurantSchedules(request, cancellationToken);
        Debug.WriteLine($"ctrl action data fetched: {Environment.CurrentManagedThreadId}");
        return Ok(res);
    }
}
