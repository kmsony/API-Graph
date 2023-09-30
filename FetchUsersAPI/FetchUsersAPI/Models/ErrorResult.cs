namespace FetchUsersAPI.Models
{
    /// <summary>
    /// Model for ErrorResult
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// property which hold the messages
        /// </summary>
        public List<string> Messages { get; set; } = new();
        /// <summary>
        /// property which hold the Source
        /// </summary>
        public string? Source { get; set; }
        /// <summary>
        /// property which hold the Exception
        /// </summary>
        public string? Exception { get; set; }
        /// <summary>
        /// property which hold the Error Id
        /// </summary>
        public string? ErrorId { get; set; }
        /// <summary>
        /// property which hold the support messages
        /// </summary>
        public string? SupportMessage { get; set; }
        /// <summary>
        /// property which hold the status code
        /// </summary>
        public int StatusCode { get; set; }
    }
}
