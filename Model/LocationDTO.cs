namespace LocationAPI.Model
{
    public class LocationDTO
    {
        public required string Name { get; set; }
        public List<BusinessDTO?>? Businesses { get; set; }        
        public BusinessDTO? Business { get; set; }
    }
}
