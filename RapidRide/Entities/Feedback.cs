using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public int? TripId { get; set; }
        public string? Comment { get; set; }
        public int? Rating { get; set; }

        [ForeignKey("TripId")]
        public Trip? Trip { get; set; }
    }
}
