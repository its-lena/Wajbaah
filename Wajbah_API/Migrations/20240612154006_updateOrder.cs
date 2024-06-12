using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajbah_API.Migrations
{
    /// <inheritdoc />
    public partial class updateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerPhoneNumber",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerPhoneNumber",
                table: "Orders",
                type: "int",
                nullable: true);
        }
    }
}
