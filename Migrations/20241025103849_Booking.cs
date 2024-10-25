using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Migrations
{
    /// <inheritdoc />
    public partial class Booking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookingType = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerName = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerPhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerEmail = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerAddress = table.Column<string>(type: "TEXT", nullable: false),
                    PetType = table.Column<int>(type: "INTEGER", nullable: false),
                    PetName = table.Column<string>(type: "TEXT", nullable: false),
                    PetAllergies = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
