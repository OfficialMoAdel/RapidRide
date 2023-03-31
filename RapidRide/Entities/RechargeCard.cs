namespace RapidRide.Entities
{
    public class RechargeCard
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Number { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public int? WalletId { get; set; }
        public virtual Wallet? Wallet { get; set; }
    }
}
