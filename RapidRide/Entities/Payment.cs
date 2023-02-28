using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{

    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public float? Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public int? TripId { get; set; }
        [ForeignKey("TripId")]
        public Trip? Trip { get; set; }
        [ForeignKey("WalletId")]
        public Wallet? Wallet { get; set; }
    }
}

