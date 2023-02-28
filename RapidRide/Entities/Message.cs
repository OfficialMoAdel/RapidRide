using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public DateTime Date { get; set; }

        public string? Content { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int CarId { get; set; }
        [ForeignKey("CarId")]
        public Car? Car { get; set; }
    }
}
