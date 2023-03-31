using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;

namespace RapidRide
{
    public class RapidRideDbContext : DbContext
    {
        public RapidRideDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Fare> Fares { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MicroBus> MicroBuses { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<RechargeCard> RechargeCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            }
            modelBuilder.Entity<Booking>()
             .HasOne(b => b.Trip)
             .WithOne(t => t.Booking)
             .HasForeignKey<Booking>(b => b.TripId);
            modelBuilder.Entity<Trip>()
             .HasOne(t => t.Fare)
             .WithOne(u => u.Trip)
             .HasForeignKey<Trip>(t => t.FareId);
            modelBuilder.Entity<PromoCode>()
               .HasOne(pc => pc.Trip)
               .WithOne(u => u.PromoCode)
               .HasForeignKey<PromoCode>(t => t.TripId);

        }
    }
}
