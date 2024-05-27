using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajbah_API.Migrations
{
    /// <inheritdoc />
    public partial class Add_ChefId_TO_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChefId",
                table: "Orders",
                type: "nvarchar(14)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ChefId",
                table: "Orders",
                column: "ChefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Chefs_ChefId",
                table: "Orders",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "ChefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Chefs_ChefId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ChefId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Orders");
        }
    }
}
