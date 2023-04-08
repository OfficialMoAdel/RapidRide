using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidRide.Migrations
{
    public partial class migrationv14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "MicroBuses");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                table: "MicroBuses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "MicroBuses",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "MicroBuses",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "MicroBuses");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "MicroBuses");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ProfilePicture",
                table: "MicroBuses",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Password",
                table: "MicroBuses",
                type: "int",
                nullable: true);
        }
    }
}
