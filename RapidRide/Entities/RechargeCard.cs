namespace RapidRide.Entities
{
    public class RechargeCard
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Number { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public int? DepositId { get; set; }
        public Deposit? Deposit { get; set; }
    }
}
