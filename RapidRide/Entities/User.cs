using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public int NationalId { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? IsActive { get; set; }
        public string? PaymentMethod { get; set; }
        public int? WalletId { get; set; }
        [ForeignKey("WalletId")]
        public Wallet? Wallet { get; set; }
        public ICollection<Trip>? Trips { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Token> Tokens { get; set; }

        //new
        public User()
        {
        }
        public class UserRegistrationModel
        {
            public string Email { get; set; }
            public int NationalId { get; set; }
            public string Password { get; set; }
        }
        public class UserLoginModel
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }


    }
}
