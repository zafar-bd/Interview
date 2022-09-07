namespace Interview.API.ModelValidations
{
    public class ModelValidationFailedResult : ObjectResult
    {
        public ModelValidationFailedResult(ModelStateDictionary modelState)
            : base(modelState)
        {
            Value = modelState.Keys
                 .SelectMany(key => modelState[key].Errors.Select(x => x.ErrorMessage))
                 .ToList();
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }

    }
}
