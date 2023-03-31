using System.ComponentModel.DataAnnotations;

namespace RapidRide.Entities
{
    public class Token
    {
        [Key]
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string JwtToken { get; set; }
        public DateTime ExpiresAt { get; set; }

        // Navigation property to the User table
        public virtual User User { get; set; }
    }

}
