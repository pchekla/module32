using System.ComponentModel.DataAnnotations.Schema;

namespace CoreStartApp.Models.Db
{
    [Table("Requests")]
    public class Request
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public required string Url { get; set; }
    }
} 