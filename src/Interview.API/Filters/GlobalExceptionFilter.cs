using Interview.Domain.Exceptions;
using System.Net;

namespace Interview.API.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostEnvironment _environment;
        private readonly ILogger logger;

        public GlobalExceptionFilter(IHostEnvironment environment,
            ILogger<GlobalExceptionFilter> logger)
        {
            _environment = environment;
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            // Log error
            logger.LogError(context.Exception, context.Exception.Message);

            // Handle exceptions
            ApiError apiError;
            if (context.Exception is ArgumentException)
            {
                var exception = context.Exception as ArgumentException;
                apiError = new ApiError()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = exception.Message,
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception is BadRequestException)
            {
                var exception = context.Exception as BadRequestException;
                apiError = new()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = exception.Message,
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception is ArgumentNullException)
            {
                var exception = context.Exception as ArgumentNullException;
                apiError = new()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = exception.Message,
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception is ApiException)
            {
                var exception = context.Exception as ApiException;
                apiError = exception.Details;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new()
                {
                    Status = (int)HttpStatusCode.Forbidden,
                    Title = "Unauthorized access",
                    Detail = context.Exception.Message
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else
            {
                // By default, errors are mapped to HTTP 500 Internal Server Error
                var exception = context.Exception;
                apiError = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = _environment.IsDevelopment() ? exception.Message : "An unexpected error occurred."
                    //Title = exception.Message
                };

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            if (_environment.IsDevelopment())
                apiError.StackTrace = context.Exception.StackTrace;

            context.Result = new JsonResult(apiError);
            base.OnException(context);
        }
    }
}
