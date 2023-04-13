using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidRide.Migrations
{
    public partial class migrationv15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RechargeCards_Wallets_WalletId",
                table: "RechargeCards");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "RechargeCards",
                newName: "DepositId");

            migrationBuilder.RenameIndex(
                name: "IX_RechargeCards_WalletId",
                table: "RechargeCards",
                newName: "IX_RechargeCards_DepositId");

            migrationBuilder.AddColumn<int>(
                name: "BusId",
                table: "Wallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Wallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "Buses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_WalletId",
                table: "Cars",
                column: "WalletId",
                unique: true,
                filter: "[WalletId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_WalletId",
                table: "Buses",
                column: "WalletId",
                unique: true,
                filter: "[WalletId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Wallets_WalletId",
                table: "Buses",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Wallets_WalletId",
                table: "Cars",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RechargeCards_Deposits_DepositId",
                table: "RechargeCards",
                column: "DepositId",
                principalTable: "Deposits",
                principalColumn: "DepositId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Wallets_WalletId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Wallets_WalletId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_RechargeCards_Deposits_DepositId",
                table: "RechargeCards");

            migrationBuilder.DropIndex(
                name: "IX_Cars_WalletId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Buses_WalletId",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "BusId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Buses");

            migrationBuilder.RenameColumn(
                name: "DepositId",
                table: "RechargeCards",
                newName: "WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_RechargeCards_DepositId",
                table: "RechargeCards",
                newName: "IX_RechargeCards_WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_RechargeCards_Wallets_WalletId",
                table: "RechargeCards",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
