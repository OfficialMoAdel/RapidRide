using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{

    public class Bus
    {
        [Key]
        public int BusId { get; set; }
        public string? BusNumber { get; set; }
        public string? LicensePlate { get; set; }
        public string? Model { get; set; }
        public int? Capacity { get; set; }
        public string? Route { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int? NationalId { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? IsActive { get; set; }
        public string? station { get; set; }
        public ICollection<Trip> Trips { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }

}
