global using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Wajbah_API.Models;

namespace Wajbah_API.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<ItemRateRecord>()
				.HasKey(c => new { c.CustomerId, c.MenuItemId });

            modelBuilder.Entity<Customer>()
				.HasIndex(c => new { c.Email, c.PhoneNumber })
				.IsUnique();

			modelBuilder.Entity<Chef>()
				.HasIndex(c => new { c.Email, c.PhoneNumber })
				.IsUnique();

			modelBuilder.Entity<Company>()
				.HasIndex(c => new { c.Email, c.PhoneNumber })
				.IsUnique();

			modelBuilder.Entity<PromoCode>()
				.HasIndex(p => p.Name)
				.IsUnique();

			modelBuilder.Entity<Order>()
				.Property(o => o.CreatedOn)
				.HasDefaultValueSql("GETDATE()");

			modelBuilder.Entity<MenuItem>()
				.Property(m => m.CreatedOn)
				.HasDefaultValueSql("GETDATE()");

			modelBuilder.Entity<MenuItem>()
				.Property(m => m.UpdatedOn )
				.HasDefaultValueSql("GETDATE()");

			modelBuilder.Entity<ExtraMenuItem>()
				.Property(e => e.CreatedOn)
				.HasDefaultValueSql("GETDATE()");

			modelBuilder.Entity<Chef>()
				.OwnsOne(c => c.Address, a =>
				{
					a.Property(a => a.Street).HasColumnName("Street").IsRequired(false);
					a.Property(a => a.City).HasColumnName("City").IsRequired(false);
					a.Property(a => a.BuildingNumber).HasColumnName("BuildingNumber").IsRequired(false);
					a.Property(a => a.FlatNumber).HasColumnName("FlatNumber").IsRequired(false);
					a.Property(a => a.Floor).HasColumnName("Floor").IsRequired(false);
					a.Property(a => a.Governorate).HasColumnName("Governorate").IsRequired(false);
				});

			modelBuilder.Entity<MenuItem>()
				.OwnsOne(m => m.SizesPrices, s =>
				{
					s.Property(s => s.PriceSmall).HasColumnName("PriceSmall");
					//s.Property(s => s.SizeSmall).HasColumnName("sizeSmall");
					s.Property(s => s.PriceMedium).HasColumnName("PriceMedium");
					//s.Property(s => s.SizeMedium).HasColumnName("SizeMedium");
					s.Property(s => s.PriceLarge).HasColumnName("PriceLarge");
					//s.Property(s => s.SizeLarge).HasColumnName("SizeLarge");
					//s.HasKey(s => new { s.Size });
				});



			//////////////////////////////RELATIONSHIPS////////////////////////////////
			//promocode-order relation (many to one)
			//modelBuilder.Entity<PromoCode>()
			//.HasMany(p => p.Orders)
			//.WithOne();
			modelBuilder.Entity<Order>()
				.HasOne(o => o.PromoCode)
				.WithMany(p => p.Orders)
				.HasForeignKey(o => o.PromoCodeId)
				.OnDelete(DeleteBehavior.SetNull);

            //chef-ExtraMenuItem relation (many to one)
            //modelBuilder.Entity<Chef>()
            //.HasMany(c => c.ExtraMenuItems)
            //.WithOne();
            modelBuilder.Entity<ExtraMenuItem>()
				.HasOne(e => e.Chef)
				.WithMany(c => c.ExtraMenuItems)
				.HasForeignKey(e => e.ChefId)
				.HasPrincipalKey(c => c.ChefId);

			//chef-MenuItem relation (many to one)  DOUBLE CHECK THISS ONE 
			//modelBuilder.Entity<Chef>()
			//.HasMany(c => c.MenuItems)
			//.WithOne()
			//.HasForeignKey(m => m.ChefId); // <=======
			modelBuilder.Entity<MenuItem>()
				.HasOne(m => m.Chef)
				.WithMany(c => c.MenuItems)
				.HasForeignKey(m => m.ChefId)
				.HasPrincipalKey(c => c.ChefId);

			//company-order relation (many to one)
			//modelBuilder.Entity<Company>()
			//.HasMany(c => c.Orders)
			//.WithOne();
			modelBuilder.Entity<Order>()
				.HasOne(o => o.Company)
				.WithMany(c => c.Orders)
				.HasForeignKey(o => o.CompanyId);

			//customer-Order relation (many to one)
			//modelBuilder.Entity<Customer>()
				//.HasMany(c => c.Orders)
				//.WithOne();
			modelBuilder.Entity<Order>()
				.HasOne(o => o.Customer)
				.WithMany(c => c.Orders)
				.HasForeignKey(o => o.CustomerId);

			//order-ExtraMenuItem relation (many to one)
			modelBuilder.Entity<Order>()
				.HasMany(o => o.ExtraMenuItems)
				.WithMany(e => e.Orders)
				.UsingEntity<OrderExtraMenuItem>(
				j => j
					.HasOne(oe => oe.ExtraMenuItem)
					.WithMany(e => e.OrderExtraMenuItems)
					.HasForeignKey(oe => oe.ExtraMenuItemId),
				j => j
					.HasOne(oe => oe.Order)
					.WithMany(o => o.OrderExtraMenuItems)
					.HasForeignKey(oe => oe.OrderId),
				j =>
				{
					j.HasKey(t => new { t.OrderId, t.ExtraMenuItemId });
				});

			//order-MenuItem relation (many to many)
			modelBuilder.Entity<Order>()
				.HasMany(o => o.MenuItems)
				.WithMany(m => m.Orders)
				.UsingEntity<OrderMenuItem>(
				j => j
					.HasOne(om => om.MenuItem)
					.WithMany(m => m.OrderMenuItems)
					.HasForeignKey(om => om.MenuItemId),
				j => j
					.HasOne(om => om.Order)
					.WithMany(o => o.OrderMenuItems)
					.HasForeignKey(om => om.OrderId),
				j =>
				{
					j.HasKey(t => new { t.OrderId, t.MenuItemId });
				});

			//chef - PromoCode relation(many to many)
			modelBuilder.Entity<Chef>()
				.HasMany(c => c.PromoCodes)
				.WithMany(p => p.Chefs)
				.UsingEntity<ChefPromoCode>(
				j => j
					.HasOne(cp => cp.PromoCode)
					.WithMany(p => p.ChefPromoCodes)
					.HasForeignKey(cp => cp.PromoCodeId),
				j => j
					.HasOne(cp => cp.Chef)
					.WithMany(c => c.ChefPromoCodes)
					.HasForeignKey(cp => cp.ChefId),
				j =>
				{
					j.HasKey(t => new { t.ChefId, t.PromoCodeId });
				});

            //Chef-Order relation (many to one)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Chef)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ChefId);

            //MenuItem-SizePrice relation (many to one)
            //modelBuilder.Entity<MenuItem>()
            //.HasMany(m => m.SizePrices)
            //.WithOne();
            //modelBuilder.Entity<SizePrice>()
            //.HasOne(s => s.MenuItem)
            //.WithMany(m => m.SizePrices)
            //.HasForeignKey(s => s.MenuItemId);

            ///////////seeding chef table/////////////



            modelBuilder.Entity<Chef>()
				.HasData(new Chef
				{
					ChefId = "30202929472263",
					ChefFirstName = "lina",
					ChefLastName = "gamal",
					PhoneNumber = 01148001373,
					Email = "lina@gmail",
					Password = "Password",
					RestaurantName = "lolla",
					BirthDate = new DateTime(2002, 1, 1),
					Description = "Description",
					Rating = 5.0,
					Wallet = 0,
					ProfilePicture = "photo"
				});

			//modelBuilder.Entity<Address>()
			//	.HasData(new Address
			//	{
			//		ChefId = "30202929472263",
			//		City = "cairo",
			//		Governorate = "cairo",
			//		BuildingNumber = "22",
			//		FlatNumber = "2",
			//		Street = "street",
			//		Floor = "1"
			//	});

			//modelBuilder.Entity<MenuItem>()
			//	.HasData(new MenuItem
			//	{
			//		ChefId = "30202929472263",
			//		Name ="blabla",
			//		EstimatedTime="bla",
			//		Description="bla",
			//		OrderingTime="bla",
			//		Category="bla",
			//		Occassions="bla",
			//		HealthyMode= true,
			//		Photo="bla",
			//		SizePrices =
			//		[
			//			Size = "bla",
			//			Price = 10
			//		]
			//	});
		}
		/////////////////////CREATING TABLES/////////////////////////////
		public DbSet<Chef> Chefs { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<ExtraMenuItem> ExtraMenuItems { get; set; }
		public DbSet<MenuItem> MenuItems { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<ItemRateRecord> ItemRateRecords { get; set; }

    }
}