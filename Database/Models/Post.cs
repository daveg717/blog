namespace Database.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Contents { get;set; }
        public DateTime Timestamp { get;set; }
        public int Category { get; set; }
    }
}