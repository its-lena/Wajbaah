using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajbah_API.Migrations
{
    /// <inheritdoc />
    public partial class AddingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chefs",
                columns: table => new
                {
                    ChefId = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChefFirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ChefLastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    RestaurantName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Wallet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Governorate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefs", x => x.ChefId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Wallet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    DeliveryFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    contract = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Wallet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsedCoupones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "PromoCodes",
                columns: table => new
                {
                    PromoCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxUsers = table.Column<int>(type: "int", nullable: false),
                    MaxLimit = table.Column<int>(type: "int", nullable: false),
                    MinLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCodes", x => x.PromoCodeId);
                });

            migrationBuilder.CreateTable(
                name: "ExtraMenuItems",
                columns: table => new
                {
                    ExtraMenuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ChefId = table.Column<string>(type: "nvarchar(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraMenuItems", x => x.ExtraMenuItemId);
                    table.ForeignKey(
                        name: "FK_ExtraMenuItems_Chefs_ChefId",
                        column: x => x.ChefId,
                        principalTable: "Chefs",
                        principalColumn: "ChefId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occassions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderingTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthyMode = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ChefId = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    PriceSmall = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceMedium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceLarge = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK_MenuItems_Chefs_ChefId",
                        column: x => x.ChefId,
                        principalTable: "Chefs",
                        principalColumn: "ChefId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Copoun = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CashDelivered = table.Column<bool>(type: "bit", nullable: false),
                    EstimatedTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PromoCodeId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_PromoCodes_PromoCodeId",
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

            migrationBuilder.InsertData(
                table: "Chefs",
                columns: new[] { "ChefId", "BirthDate", "ChefFirstName", "ChefLastName", "Description", "Email", "Password", "PhoneNumber", "ProfilePicture", "Rating", "RestaurantName", "Wallet" },
                values: new object[] { "30202929472263", new DateTime(2002, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lina", "gamal", "Description", "lina@gmail", "Password", 1148001373, "photo", 5.5m, "lolla", 0m });

            migrationBuilder.CreateIndex(
                name: "IX_ChefPromoCode_PromoCodeId",
                table: "ChefPromoCode",
                column: "PromoCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Chefs_Email_PhoneNumber",
                table: "Chefs",
                columns: new[] { "Email", "PhoneNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Email_PhoneNumber",
                table: "Companies",
                columns: new[] { "Email", "PhoneNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email_PhoneNumber",
                table: "Customers",
                columns: new[] { "Email", "PhoneNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExtraMenuItems_ChefId",
                table: "ExtraMenuItems",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ChefId",
                table: "MenuItems",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderExtraMenuItem_ExtraMenuItemId",
                table: "OrderExtraMenuItem",
                column: "ExtraMenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMenuItem_MenuItemId",
                table: "OrderMenuItem",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CompanyId",
                table: "Orders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PromoCodeId",
                table: "Orders",
                column: "PromoCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_Name",
                table: "PromoCodes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChefPromoCode");

            migrationBuilder.DropTable(
                name: "OrderExtraMenuItem");

            migrationBuilder.DropTable(
                name: "OrderMenuItem");

            migrationBuilder.DropTable(
                name: "ExtraMenuItems");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Chefs");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PromoCodes");
        }
    }
}
