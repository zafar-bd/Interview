namespace Interview.API.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public IMediator Mediator { get; set; }
    }
}
