using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajbah_API.Migrations
{
    /// <inheritdoc />
    public partial class ChefRating_to_Double : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Chefs",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Chefs",
                keyColumn: "ChefId",
                keyValue: "30202929472263",
                column: "Rating",
                value: 5.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Chefs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Chefs",
                keyColumn: "ChefId",
                keyValue: "30202929472263",
                column: "Rating",
                value: 5.5m);
        }
    }
}
