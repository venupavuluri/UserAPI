namespace UserAPI.Model
{
    /// <summary>
    /// Error response model
    /// </summary>
    public class ErrorResponseModel
    {
        /// <summary>
        /// Current datetime stamp
        /// </summary>
        public DateTimeOffset TimeStampInUTC => DateTimeOffset.UtcNow;
        
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// Inner Error message
        /// </summary>
        public string ErrorDetails { get; set; }
        
        /// <summary>
        /// Error status code
        /// </summary>
        public int StatusCode { get; set; }
    }
}
