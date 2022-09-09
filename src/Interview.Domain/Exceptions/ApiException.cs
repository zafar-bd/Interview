using System.Net;

namespace Interview.Domain.Exceptions
{
    public class ApiException : Exception
    {
        public ApiError Details { get; set; }

        public ApiException(string message)
            : base(message)
        {
            Details = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Detail = message
            };
        }

        public ApiException(int errorCode, string message)
            : base(message)
        {
            Details = new()
            {
                Status = errorCode,
                Detail = message
            };
        }

        public ApiException(string message, ApiError details)
            : base(message)
        {
            Details = details;
        }
    }
}
