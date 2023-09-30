using System.Net;
namespace FetchUsersAPI.Exceptions
{
    /// <summary>
    /// Each exception will inherit from CustomException
    /// </summary>
    public class NotFoundException: CustomException
    {
        /// <summary>
        /// Handling NotFound Exception
        /// </summary>
        /// <param name="message"></param>
        public NotFoundException(string message)
           : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}
