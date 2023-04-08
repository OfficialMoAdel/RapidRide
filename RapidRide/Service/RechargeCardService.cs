using RapidRide.Entities;

namespace RapidRide.Service
{
    public class RechargeCardService
    {
        private readonly RapidRideDbContext _context;

        public RechargeCardService(RapidRideDbContext context)
        {
            _context = context;
        }

        public async Task<List<RechargeCard>> GenerateRechargeCards(int count, string category)
        {
            var rechargeCards = new List<RechargeCard>();

            for (int i = 0; i < count; i++)
            {
                var number = GenerateUniqueRechargeCardNumber();
                var rechargeCard = new RechargeCard
                {
                    Category = category,
                    Number = number,
                    IsActive = true,
                    Date = DateTime.UtcNow
                };
                _context.RechargeCards.Add(rechargeCard);
                rechargeCards.Add(rechargeCard);
            }

            await _context.SaveChangesAsync();
            return rechargeCards;
        }

        private string GenerateUniqueRechargeCardNumber()
        {
            string number;
            do
            {
                number = GenerateRechargeCardNumber();
            } while (_context.RechargeCards.Any(rc => rc.Number == number));

            return number;
        }

        private string GenerateRechargeCardNumber()
        {
            var random = new Random();
            return $"{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}";
        }
    }
}
