using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajbah_API.Migrations
{
    /// <inheritdoc />
    public partial class relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PromoCodeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChefId",
                table: "MenuItems",
                type: "nvarchar(14)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChefId",
                table: "ExtraMenuItems",
                type: "nvarchar(14)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ChefPromoCode",
                columns: table => new
                {
                    PromoCodeId = table.Column<int>(type: "int", nullable: false),
                    ChefId = table.Column<string>(type: "nvarchar(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChefPromoCode", x => new { x.ChefId, x.PromoCodeId });
                    table.ForeignKey(
                        name: "FK_ChefPromoCode_Chefs_ChefId",
                        column: x => x.ChefId,
                        principalTable: "Chefs",
                        principalColumn: "ChefId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChefPromoCode_PromoCodes_PromoCodeId",
                        column: x => x.PromoCodeId,
                        principalTable: "PromoCodes",
                        principalColumn: "PromoCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderExtraMenuItem",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ExtraMenuItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderExtraMenuItem", x => new { x.OrderId, x.ExtraMenuItemId });
                    table.ForeignKey(
                        name: "FK_OrderExtraMenuItem_ExtraMenuItems_ExtraMenuItemId",
                        column: x => x.ExtraMenuItemId,
                        principalTable: "ExtraMenuItems",
                        principalColumn: "ExtraMenuItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderExtraMenuItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderMenuItem",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMenuItem", x => new { x.OrderId, x.MenuItemId });
                    table.ForeignKey(
                        name: "FK_OrderMenuItem_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderMenuItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CompanyId",
                table: "Orders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PromoCodeId",
                table: "Orders",
                column: "PromoCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ChefId",
                table: "MenuItems",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraMenuItems_ChefId",
                table: "ExtraMenuItems",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_ChefPromoCode_PromoCodeId",
                table: "ChefPromoCode",
                column: "PromoCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderExtraMenuItem_ExtraMenuItemId",
                table: "OrderExtraMenuItem",
                column: "ExtraMenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMenuItem_MenuItemId",
                table: "OrderMenuItem",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraMenuItems_Chefs_ChefId",
                table: "ExtraMenuItems",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "ChefId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Chefs_ChefId",
                table: "MenuItems",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "ChefId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Companies_CompanyId",
                table: "Orders",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CompanyId",
                table: "Orders",
                column: "CompanyId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PromoCodes_PromoCodeId",
                table: "Orders",
                column: "PromoCodeId",
                principalTable: "PromoCodes",
                principalColumn: "PromoCodeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraMenuItems_Chefs_ChefId",
                table: "ExtraMenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Chefs_ChefId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Companies_CompanyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CompanyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PromoCodes_PromoCodeId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "ChefPromoCode");

            migrationBuilder.DropTable(
                name: "OrderExtraMenuItem");

            migrationBuilder.DropTable(
                name: "OrderMenuItem");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CompanyId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PromoCodeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_ChefId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_ExtraMenuItems_ChefId",
                table: "ExtraMenuItems");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "ExtraMenuItems");
        }
    }
}
