using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidRide.Entities
{
    public class PromoCode
    {
        [Key]
        public int PromoCodeId { get; set; }
        public string? Code { get; set; }
        public float? Discount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? Count { get; set; }


        [ForeignKey("TripId")]
        public int TripId { get; set; }
        public Trip? Trip { get; set; }


    }
}
