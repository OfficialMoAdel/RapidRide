
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

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
        //public int? Password { get; set; }
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

        public User(string email, string phoneNumber, string password)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        // Helper method to create salt and hash password
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var sha256 = SHA256.Create())
            {
                passwordSalt = new byte[32];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(passwordSalt);
                }
                var saltedPassword = $"{Convert.ToBase64String(passwordSalt)}{password}";
                passwordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            }
        }

        // Helper method to hash password with given salt
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = $"{Convert.ToBase64String(PasswordSalt)}{password}";
                var hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashedPassword);
            }
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
