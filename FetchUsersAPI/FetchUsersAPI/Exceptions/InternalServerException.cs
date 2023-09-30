using System.Net;
namespace FetchUsersAPI.Exceptions
{
    /// <summary>
    /// Each exception will inherit from CustomException
    /// </summary>
    public class InternalServerException: CustomException
    {
        /// <summary>
        /// Handling server exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        public InternalServerException(string message, List<string>? errors = default)
            : base(message, errors, HttpStatusCode.InternalServerError) { }
    }
}
