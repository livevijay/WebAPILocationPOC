namespace LocationAPI.Model
{
    /// <summary>
    /// This is common object to response
    /// </summary>
    /// <typeparam name="T">Payload type to response</typeparam>
    public class ResultDTO <T>
    {
        /// <summary>
        /// Request / Operation status
        /// </summary>
        public bool Success  { get; set; }
        /// <summary>
        /// Success / Failed message
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// Data transfer to requester
        /// </summary>
        public T? Payload { get; set; }

    }
}
