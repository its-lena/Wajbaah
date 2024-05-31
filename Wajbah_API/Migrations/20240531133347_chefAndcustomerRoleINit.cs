using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajbah_API.Migrations
{
    /// <inheritdoc />
    public partial class chefAndcustomerRoleINit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chefs",
                keyColumn: "ChefId",
                keyValue: "30202929472263",
                column: "Role",
                value: "chef");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Chefs",
                keyColumn: "ChefId",
                keyValue: "30202929472263",
                column: "Role",
                value: null);
        }
    }
}
