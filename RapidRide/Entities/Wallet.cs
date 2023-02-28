using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }
        public float? Balance { get; set; }
        public string? Currency { get; set; }
        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<Deposit>? Deposits { get; set; }
        public ICollection<Withdrawal>? Withdrawals { get; set; }
    }

}
