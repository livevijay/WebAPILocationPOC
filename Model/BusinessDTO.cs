namespace LocationAPI.Model
{
    public class BusinessDTO
    {
        /// <summary>
        /// Business Name
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Business Type
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Business opening time
        /// </summary>
        public TimeSpan? Opening { get; set; }
        /// <summary>
        /// Business closing time
        /// </summary>
        public TimeSpan? Closing { get; set; }


    }
}
