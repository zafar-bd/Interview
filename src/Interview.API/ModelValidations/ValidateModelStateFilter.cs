namespace Interview.API.ModelValidations
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ModelValidationFailedResult(context.ModelState);
            }
        }
    }
}
