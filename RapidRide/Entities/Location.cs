using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [ForeignKey("TripId")]
        public int TripId { get; set; }
        public Trip? Trip { get; set; }
    }
}
