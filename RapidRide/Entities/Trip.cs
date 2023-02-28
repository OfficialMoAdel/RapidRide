using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }
        public DateTime PickupTime { get; set; }
        public string? PickupLocation { get; set; }
        public string? DropoffLocation { get; set; }
        public float? Distance { get; set; }
        public float? Cost { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [ForeignKey("CarId")]
        public int CarId { get; set; }
        public Car? Car { get; set; }

        [ForeignKey("FareId")]
        public int FareId { get; set; }
        public Fare? Fare { get; set; }

        [ForeignKey("BookingId")]
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }

        [ForeignKey("BusId")]
        public int BusId { get; set; }
        public Bus Bus { get; set; }

        [ForeignKey("PromoCodeId")]
        public int PromoCodeId { get; set; }
        public PromoCode? PromoCode { get; set; }

        [ForeignKey("PaymentId")]
        public int PaymentId { get; set; }
        public Payment? Payment { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }

    }
}
