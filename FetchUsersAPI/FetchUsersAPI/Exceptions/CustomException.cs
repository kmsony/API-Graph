using System.Net;
namespace FetchUsersAPI.Exceptions
{
    /// <summary>
    /// Custom Exception inherit from Exception, this is the base class for other exceptions.
    /// </summary>
    public class CustomException: Exception
    {
        /// <summary>
        /// property which hold ErrorMessages
        /// </summary>
        public List<string>? ErrorMessages { get; }

        /// <summary>
        /// property which hold StatusCode
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Custom Exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        /// <param name="statusCode"></param>
        public CustomException(string message, List<string>? errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            ErrorMessages = errors;
            StatusCode = statusCode;
        }
    }
}
