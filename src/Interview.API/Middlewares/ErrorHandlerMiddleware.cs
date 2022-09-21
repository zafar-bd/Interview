using System.Net;

namespace Interview.API.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            //var responseModel = ApiResponse<string>.Fail(error.Message);
            //switch (error)
            //{
            //    case SomeException e:
            //        // custom application error
            //        response.StatusCode = (int)HttpStatusCode.BadRequest;
            //        break;
            //    case KeyNotFoundException e:
            //        // not found error
            //        response.StatusCode = (int)HttpStatusCode.NotFound;
            //        break;
            //    default:
            //        // unhandled error
            //        response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //        break;
            //}
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(response);
            await response.WriteAsync(result);
        }
    }
}
