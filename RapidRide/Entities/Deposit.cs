using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class Deposit
    {
        [Key]
        public int DepositId { get; set; }
        public float? Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Type { get; set; }// "Recharge Card", "PayPal", "Credit Card", etc.
        public int WalletId { get; set; }
        [ForeignKey("WalletId")]
        public Wallet? Wallet { get; set; }

        public ICollection<RechargeCard> RechargeCards { get; set; }
    }
}
