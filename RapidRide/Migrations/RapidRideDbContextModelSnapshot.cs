﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RapidRide;

#nullable disable

namespace RapidRide.Migrations
{
    [DbContext(typeof(RapidRideDbContext))]
    partial class RapidRideDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RapidRide.Entities.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"), 1L, 1);

                    b.Property<string>("BookingLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BookingTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BusId")
                        .HasColumnType("int");

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("BusId");

                    b.HasIndex("CarId");

                    b.HasIndex("TripId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("RapidRide.Entities.Bus", b =>
                {
                    b.Property<int>("BusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NationalId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Route")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("station")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BusId");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("RapidRide.Entities.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NationalId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("RapidRide.Entities.Deposit", b =>
                {
                    b.Property<int>("DepositId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepositId"), 1L, 1);

                    b.Property<float?>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("DepositId");

                    b.HasIndex("WalletId");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("RapidRide.Entities.Fare", b =>
                {
                    b.Property<int>("FareId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FareId"), 1L, 1);

                    b.Property<float?>("BaseFare")
                        .HasColumnType("real");

                    b.Property<float?>("PerKilometerFare")
                        .HasColumnType("real");

                    b.Property<float?>("PerMinuteFare")
                        .HasColumnType("real");

                    b.Property<float?>("TotalFare")
                        .HasColumnType("real");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("FareId");

                    b.ToTable("Fares");
                });

            modelBuilder.Entity("RapidRide.Entities.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<int?>("TripId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("TripId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("RapidRide.Entities.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("LocationId");

                    b.HasIndex("TripId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("RapidRide.Entities.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"), 1L, 1);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MessageId");

                    b.HasIndex("CarId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("RapidRide.Entities.MicroBus", b =>
                {
                    b.Property<int>("MicroBusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MicroBusId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MicroBusNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NationalId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Route")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("station")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MicroBusId");

                    b.ToTable("MicroBuses");
                });

            modelBuilder.Entity("RapidRide.Entities.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"), 1L, 1);

                    b.Property<float?>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TripId")
                        .HasColumnType("int");

                    b.Property<int?>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("TripId")
                        .IsUnique()
                        .HasFilter("[TripId] IS NOT NULL");

                    b.HasIndex("WalletId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("RapidRide.Entities.PromoCode", b =>
                {
                    b.Property<int>("PromoCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromoCodeId"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Count")
                        .HasColumnType("int");

                    b.Property<float?>("Discount")
                        .HasColumnType("real");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("PromoCodeId");

                    b.HasIndex("TripId")
                        .IsUnique();

                    b.ToTable("PromoCodes");
                });

            modelBuilder.Entity("RapidRide.Entities.RechargeCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WalletId");

                    b.ToTable("RechargeCards");
                });

            modelBuilder.Entity("RapidRide.Entities.Token", b =>
                {
                    b.Property<int>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenId"), 1L, 1);

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("JwtToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TokenId");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("RapidRide.Entities.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TripId"), 1L, 1);

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("BusId")
                        .HasColumnType("int");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<float?>("Cost")
                        .HasColumnType("real");

                    b.Property<float?>("Distance")
                        .HasColumnType("real");

                    b.Property<string>("DropoffLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FareId")
                        .HasColumnType("int");

                    b.Property<int?>("MicroBusId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<string>("PickupLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PickupTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PromoCodeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TripId");

                    b.HasIndex("BusId");

                    b.HasIndex("CarId");

                    b.HasIndex("FareId")
                        .IsUnique();

                    b.HasIndex("MicroBusId");

                    b.HasIndex("UserId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("RapidRide.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NationalId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("WalletId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RapidRide.Entities.Wallet", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WalletId"), 1L, 1);

                    b.Property<float?>("Balance")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("WalletId");

                    b.HasIndex("UserId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("RapidRide.Entities.Withdrawal", b =>
                {
                    b.Property<int>("WithdrawalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WithdrawalId"), 1L, 1);

                    b.Property<float?>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("WithdrawalId");

                    b.HasIndex("WalletId");

                    b.ToTable("Withdrawals");
                });

            modelBuilder.Entity("RapidRide.Entities.Booking", b =>
                {
                    b.HasOne("RapidRide.Entities.Bus", null)
                        .WithMany("Bookings")
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RapidRide.Entities.Car", null)
                        .WithMany("Bookings")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RapidRide.Entities.Trip", "Trip")
                        .WithOne("Booking")
                        .HasForeignKey("RapidRide.Entities.Booking", "TripId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RapidRide.Entities.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Trip");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RapidRide.Entities.Deposit", b =>
                {
                    b.HasOne("RapidRide.Entities.Wallet", "Wallet")
                        .WithMany("Deposits")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("RapidRide.Entities.Feedback", b =>
                {
                    b.HasOne("RapidRide.Entities.Trip", "Trip")
                        .WithMany("Feedbacks")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("RapidRide.Entities.Location", b =>
                {
                    b.HasOne("RapidRide.Entities.Trip", "Trip")
                        .WithMany()
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("RapidRide.Entities.Message", b =>
                {
                    b.HasOne("RapidRide.Entities.Car", "Car")
                        .WithMany("Messages")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RapidRide.Entities.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RapidRide.Entities.Payment", b =>
                {
                    b.HasOne("RapidRide.Entities.Trip", "Trip")
                        .WithOne("Payment")
                        .HasForeignKey("RapidRide.Entities.Payment", "TripId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RapidRide.Entities.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Trip");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("RapidRide.Entities.PromoCode", b =>
                {
                    b.HasOne("RapidRide.Entities.Trip", "Trip")
                        .WithOne("PromoCode")
                        .HasForeignKey("RapidRide.Entities.PromoCode", "TripId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("RapidRide.Entities.RechargeCard", b =>
                {
                    b.HasOne("RapidRide.Entities.Wallet", "Wallet")
                        .WithMany("RechargeCards")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("RapidRide.Entities.Token", b =>
                {
                    b.HasOne("RapidRide.Entities.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RapidRide.Entities.Trip", b =>
                {
                    b.HasOne("RapidRide.Entities.Bus", "Bus")
                        .WithMany("Trips")
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RapidRide.Entities.Car", "Car")
                        .WithMany("Trips")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RapidRide.Entities.Fare", "Fare")
                        .WithOne("Trip")
                        .HasForeignKey("RapidRide.Entities.Trip", "FareId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RapidRide.Entities.MicroBus", null)
                        .WithMany("Trips")
                        .HasForeignKey("MicroBusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RapidRide.Entities.User", "User")
                        .WithMany("Trips")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Bus");

                    b.Navigation("Car");

                    b.Navigation("Fare");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RapidRide.Entities.User", b =>
                {
                    b.HasOne("RapidRide.Entities.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("RapidRide.Entities.Wallet", b =>
                {
                    b.HasOne("RapidRide.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RapidRide.Entities.Withdrawal", b =>
                {
                    b.HasOne("RapidRide.Entities.Wallet", "Wallet")
                        .WithMany("Withdrawals")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("RapidRide.Entities.Bus", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Trips");
                });

            modelBuilder.Entity("RapidRide.Entities.Car", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Messages");

                    b.Navigation("Trips");
                });

            modelBuilder.Entity("RapidRide.Entities.Fare", b =>
                {
                    b.Navigation("Trip");
                });

            modelBuilder.Entity("RapidRide.Entities.MicroBus", b =>
                {
                    b.Navigation("Trips");
                });

            modelBuilder.Entity("RapidRide.Entities.Trip", b =>
                {
                    b.Navigation("Booking");

                    b.Navigation("Feedbacks");

                    b.Navigation("Payment");

                    b.Navigation("PromoCode");
                });

            modelBuilder.Entity("RapidRide.Entities.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Messages");

                    b.Navigation("Tokens");

                    b.Navigation("Trips");
                });

            modelBuilder.Entity("RapidRide.Entities.Wallet", b =>
                {
                    b.Navigation("Deposits");

                    b.Navigation("RechargeCards");

                    b.Navigation("Withdrawals");
                });
#pragma warning restore 612, 618
        }
    }
}
