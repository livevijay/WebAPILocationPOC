namespace LocationAPI.Model
{
    public class LocationDTO
    {
        /// <summary>
        /// Location Name
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// List of business based on location
        /// </summary>
        public List<BusinessDTO?>? Businesses { get; set; }        
        /// <summary>
        /// Business
        /// </summary>
        public BusinessDTO? Business { get; set; }
    }
}
