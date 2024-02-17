namespace LocationAPI.Model
{
    public class ResultDTO <T>
    {
        public bool Success  { get; set; }
        public string? Message { get; set; }
        public T? Payload { get; set; }

    }
}
