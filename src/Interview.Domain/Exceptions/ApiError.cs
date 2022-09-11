using Microsoft.AspNetCore.Mvc;

namespace Interview.Domain.Exceptions;

public class ApiError : ProblemDetails
{
    public string? StackTrace { get; set; }
}
