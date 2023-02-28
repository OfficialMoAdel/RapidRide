using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingTime { get; set; }
        public string? BookingLocation { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("TripId")]
        public int TripId { get; set; }
        public Trip? Trip { get; set; }
    }
}
