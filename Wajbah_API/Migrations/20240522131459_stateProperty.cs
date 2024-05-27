using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajbah_API.Migrations
{
    /// <inheritdoc />
    public partial class stateProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Chefs_ChefId",
                table: "MenuItems");

            migrationBuilder.AlterColumn<string>(
                name: "ChefId",
                table: "MenuItems",
                type: "nvarchar(14)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "Chefs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Chefs",
                keyColumn: "ChefId",
                keyValue: "30202929472263",
                column: "State",
                value: false);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Chefs_ChefId",
                table: "MenuItems",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "ChefId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Chefs_ChefId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Chefs");

            migrationBuilder.AlterColumn<string>(
                name: "ChefId",
                table: "MenuItems",
                type: "nvarchar(14)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Chefs_ChefId",
                table: "MenuItems",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "ChefId");
        }
    }
}
