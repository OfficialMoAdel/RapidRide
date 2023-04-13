using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class Withdrawal
    {
        [Key]
        public int WithdrawalId { get; set; }
        public float? Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Type { get; set; }// "Bank Transfer", "PayPal", "Credit Card", etc.
        public int WalletId { get; set; }
        [ForeignKey("WalletId")]
        public Wallet? Wallet { get; set; }
    }
}
