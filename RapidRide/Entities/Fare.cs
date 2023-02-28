using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class Fare
    {
        [Key]
        public int FareId { get; set; }
        public float? BaseFare { get; set; }
        public float? PerKilometerFare { get; set; }
        public float? PerMinuteFare { get; set; }
        public float? TotalFare { get; set; }

        [ForeignKey("TripId")]
        public int TripId { get; set; }
        public Trip? Trip { get; set; }

    }
}
