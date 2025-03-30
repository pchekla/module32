namespace CoreStartApp.Models.Db
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public required string From { get; set; }
        public required string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}