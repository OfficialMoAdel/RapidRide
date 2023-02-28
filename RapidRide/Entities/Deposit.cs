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

        public int WalletId { get; set; }
        [ForeignKey("WalletId")]
        public Wallet? Wallet { get; set; }
    }
}
