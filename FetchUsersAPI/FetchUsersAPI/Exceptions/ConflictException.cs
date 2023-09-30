using System.Net;
namespace FetchUsersAPI.Exceptions
{
    /// <summary>
    ///  Each exception will inherit from CustomException
    /// </summary>
    public class ConflictException: CustomException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ConflictException(string message)
            : base(message, null, HttpStatusCode.Conflict) { }
    }
}
