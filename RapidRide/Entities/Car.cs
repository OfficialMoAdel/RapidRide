using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public string? LicensePlate { get; set; }
        public string? Model { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
        public int? NationalId { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int? Password { get; set; }
        public int? IsActive { get; set; }
        public ICollection<Trip>? Trips { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Booking>? Bookings { get; set; }

    }
}
