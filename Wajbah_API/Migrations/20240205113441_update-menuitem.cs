using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajbah_API.Migrations
{
    /// <inheritdoc />
    public partial class updatemenuitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Chefs",
                keyColumn: "ChefId",
                keyValue: "30202929472263");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Chefs",
                columns: new[] { "ChefId", "BirthDate", "ChefFirstName", "ChefLastName", "Description", "Email", "Password", "PhoneNumber", "ProfilePicture", "Rating", "RestaurantName", "Wallet" },
                values: new object[] { "30202929472263", new DateTime(2002, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lina", "gamal", "Description", "lina@gmail", "Password", 1148001373, "photo", 5.5m, "lolla", 0m });
        }
    }
}
