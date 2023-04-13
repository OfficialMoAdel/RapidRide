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
        [ForeignKey("CarId")]
        public int CarId { get; set; }
        public Car? Car { get; set; }
        [ForeignKey("BusId")]
        public int BusId { get; set; }
        public Bus Bus { get; set; }

        public ICollection<Deposit>? Deposits { get; set; }
        public ICollection<Withdrawal>? Withdrawals { get; set; }
    }

}
