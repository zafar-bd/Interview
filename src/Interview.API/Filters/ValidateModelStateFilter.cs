namespace Interview.API.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var modelErrors = context.ModelState.Keys
                     .SelectMany(key => context.ModelState[key].Errors.Select(x => x.ErrorMessage))
                     .ToList();

                context.HttpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

                context.Result = new JsonResult(new ProblemDetails
                {
                    Status = context.HttpContext.Response.StatusCode,
                    Title = "Model Validation Failed",
                    Detail = string.Join(',', modelErrors)
                });
            }
        }
    }
}
