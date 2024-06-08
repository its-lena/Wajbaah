﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wajbah_API.Data;

#nullable disable

namespace Wajbah_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Wajbah_API.Models.Chef", b =>
                {
                    b.Property<string>("ChefId")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ChefFirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("ChefLastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("RestaurantName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<decimal>("Wallet")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ChefId");

                    b.HasIndex("Email", "PhoneNumber")
                        .IsUnique();

                    b.ToTable("Chefs");

                    b.HasData(
                        new
                        {
                            ChefId = "30202929472263",
                            Active = false,
                            BirthDate = new DateTime(2002, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ChefFirstName = "lina",
                            ChefLastName = "gamal",
                            Description = "Description",
                            Email = "lina@gmail",
                            Password = "Password",
                            PhoneNumber = 1148001373,
                            ProfilePicture = "photo",
                            Rating = 5.0,
                            RestaurantName = "lolla",
                            Role = "chef",
                            State = false,
                            Wallet = 0m
                        });
                });

            modelBuilder.Entity("Wajbah_API.Models.ChefPromoCode", b =>
                {
                    b.Property<string>("ChefId")
                        .HasColumnType("nvarchar(14)");

                    b.Property<int>("PromoCodeId")
                        .HasColumnType("int");

                    b.HasKey("ChefId", "PromoCodeId");

                    b.HasIndex("PromoCodeId");

                    b.ToTable("ChefPromoCode");
                });

            modelBuilder.Entity("Wajbah_API.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("DeliveryFees")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("Wallet")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("contract")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.HasIndex("Email", "PhoneNumber")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Wajbah_API.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Favourites")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<string>("UsedCoupones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Wallet")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CustomerId");

                    b.HasIndex("Email", "PhoneNumber")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Wajbah_API.Models.ExtraMenuItem", b =>
                {
                    b.Property<int>("ExtraMenuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExtraMenuItemId"));

                    b.Property<string>("ChefId")
                        .HasColumnType("nvarchar(14)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ExtraMenuItemId");

                    b.HasIndex("ChefId");

                    b.ToTable("ExtraMenuItems");
                });

            modelBuilder.Entity("Wajbah_API.Models.ItemRateRecord", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("CustomerId", "MenuItemId");

                    b.HasIndex("MenuItemId");

                    b.ToTable("ItemRateRecords");
                });

            modelBuilder.Entity("Wajbah_API.Models.MenuItem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuItemId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChefId")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstimatedTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HealthyMode")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Occassions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderingTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<string>("RestaurantPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("MenuItemId");

                    b.HasIndex("ChefId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("Wajbah_API.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<bool>("CashDelivered")
                        .HasColumnType("bit");

                    b.Property<string>("ChefId")
                        .HasColumnType("nvarchar(14)");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Copoun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal>("DeliveryFees")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("DeliveryNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EstimatedTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PromoCodeId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Pending");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.HasIndex("ChefId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PromoCodeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Wajbah_API.Models.OrderExtraMenuItem", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ExtraMenuItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ExtraMenuItemId");

                    b.HasIndex("ExtraMenuItemId");

                    b.ToTable("OrderExtraMenuItem");
                });

            modelBuilder.Entity("Wajbah_API.Models.OrderMenuItem", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId", "MenuItemId");

                    b.HasIndex("MenuItemId");

                    b.ToTable("OrderMenuItem");
                });

            modelBuilder.Entity("Wajbah_API.Models.PromoCode", b =>
                {
                    b.Property<int>("PromoCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromoCodeId"));

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxLimit")
                        .HasColumnType("int");

                    b.Property<int>("MaxUsers")
                        .HasColumnType("int");

                    b.Property<int>("MinLimit")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PromoCodeId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PromoCodes");
                });

            modelBuilder.Entity("Wajbah_API.Models.Chef", b =>
                {
                    b.OwnsOne("Wajbah_API.Models.Address", "Address", b1 =>
                        {
                            b1.Property<string>("ChefId")
                                .HasColumnType("nvarchar(14)");

                            b1.Property<string>("BuildingNumber")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("BuildingNumber");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("FlatNumber")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("FlatNumber");

                            b1.Property<string>("Floor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Floor");

                            b1.Property<string>("Governorate")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Governorate");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Street");

                            b1.HasKey("ChefId");

                            b1.ToTable("Chefs");

                            b1.WithOwner()
                                .HasForeignKey("ChefId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Wajbah_API.Models.ChefPromoCode", b =>
                {
                    b.HasOne("Wajbah_API.Models.Chef", "Chef")
                        .WithMany("ChefPromoCodes")
                        .HasForeignKey("ChefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wajbah_API.Models.PromoCode", "PromoCode")
                        .WithMany("ChefPromoCodes")
                        .HasForeignKey("PromoCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chef");

                    b.Navigation("PromoCode");
                });

            modelBuilder.Entity("Wajbah_API.Models.ExtraMenuItem", b =>
                {
                    b.HasOne("Wajbah_API.Models.Chef", "Chef")
                        .WithMany("ExtraMenuItems")
                        .HasForeignKey("ChefId");

                    b.Navigation("Chef");
                });

            modelBuilder.Entity("Wajbah_API.Models.ItemRateRecord", b =>
                {
                    b.HasOne("Wajbah_API.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wajbah_API.Models.MenuItem", "MenuItem")
                        .WithMany()
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("MenuItem");
                });

            modelBuilder.Entity("Wajbah_API.Models.MenuItem", b =>
                {
                    b.HasOne("Wajbah_API.Models.Chef", "Chef")
                        .WithMany("MenuItems")
                        .HasForeignKey("ChefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Wajbah_API.Models.SizesPrice", "SizesPrices", b1 =>
                        {
                            b1.Property<int>("MenuItemId")
                                .HasColumnType("int");

                            b1.Property<decimal>("PriceLarge")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("PriceLarge");

                            b1.Property<decimal>("PriceMedium")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("PriceMedium");

                            b1.Property<decimal>("PriceSmall")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("PriceSmall");

                            b1.HasKey("MenuItemId");

                            b1.ToTable("MenuItems");

                            b1.WithOwner()
                                .HasForeignKey("MenuItemId");
                        });

                    b.Navigation("Chef");

                    b.Navigation("SizesPrices")
                        .IsRequired();
                });

            modelBuilder.Entity("Wajbah_API.Models.Order", b =>
                {
                    b.HasOne("Wajbah_API.Models.Chef", "Chef")
                        .WithMany("Orders")
                        .HasForeignKey("ChefId");

                    b.HasOne("Wajbah_API.Models.Company", "Company")
                        .WithMany("Orders")
                        .HasForeignKey("CompanyId");

                    b.HasOne("Wajbah_API.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wajbah_API.Models.PromoCode", "PromoCode")
                        .WithMany("Orders")
                        .HasForeignKey("PromoCodeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Chef");

                    b.Navigation("Company");

                    b.Navigation("Customer");

                    b.Navigation("PromoCode");
                });

            modelBuilder.Entity("Wajbah_API.Models.OrderExtraMenuItem", b =>
                {
                    b.HasOne("Wajbah_API.Models.ExtraMenuItem", "ExtraMenuItem")
                        .WithMany("OrderExtraMenuItems")
                        .HasForeignKey("ExtraMenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wajbah_API.Models.Order", "Order")
                        .WithMany("OrderExtraMenuItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExtraMenuItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Wajbah_API.Models.OrderMenuItem", b =>
                {
                    b.HasOne("Wajbah_API.Models.MenuItem", "MenuItem")
                        .WithMany("OrderMenuItems")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wajbah_API.Models.Order", "Order")
                        .WithMany("OrderMenuItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Wajbah_API.Models.Chef", b =>
                {
                    b.Navigation("ChefPromoCodes");

                    b.Navigation("ExtraMenuItems");

                    b.Navigation("MenuItems");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Wajbah_API.Models.Company", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Wajbah_API.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Wajbah_API.Models.ExtraMenuItem", b =>
                {
                    b.Navigation("OrderExtraMenuItems");
                });

            modelBuilder.Entity("Wajbah_API.Models.MenuItem", b =>
                {
                    b.Navigation("OrderMenuItems");
                });

            modelBuilder.Entity("Wajbah_API.Models.Order", b =>
                {
                    b.Navigation("OrderExtraMenuItems");

                    b.Navigation("OrderMenuItems");
                });

            modelBuilder.Entity("Wajbah_API.Models.PromoCode", b =>
                {
                    b.Navigation("ChefPromoCodes");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
