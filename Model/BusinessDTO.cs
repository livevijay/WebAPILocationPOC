namespace LocationAPI.Model
{
    public class BusinessDTO
    {
        public required string Name { get; set; }
        public string? Type { get; set; }

        public TimeSpan? Opening { get; set; }
        public TimeSpan? Closing { get; set; }


    }
}
