﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wajbah_API.Models
{
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderId { get; set; }
		public string Notes { get; set; }
		/*[Required]
		public decimal TotalPrice
		{
			get { return OrderMenuItems.Sum(order => order.TotalItem) + OrderExtraMenuItems.Sum(e => e.TotalItem) + DeliveryFees; }
		}
		[Required]
        public decimal SubTotal { 
			get { return OrderMenuItems.Sum(order => order.TotalItem) + OrderExtraMenuItems.Sum(e => e.TotalItem) ; } 
		}
		*/
		[Required]
		public decimal DeliveryFees { get; set; }
		[Required]
		public int DeliveryNumber { get; set; }
		[Required]
		public DateTime CreatedOn { get; set; }
		[Required]
		public DateTime DeliveryTime { get; set; }
		public string Status { get; set; }
		public string Copoun { get; set; }
		public bool CashDelivered { get; set; }
		public string EstimatedTime { get; set; }
        //promocode-order relation (many to one)
        public int PromoCodeId { get; set; }
        public PromoCode PromoCode { get; set; }
        //order-ExtraMenuItem relation (many to many)
        public ICollection<ExtraMenuItem> ExtraMenuItems { get; set; }
        public List<OrderExtraMenuItem> OrderExtraMenuItems { get; set; }

        //Company-Order relation (many to one)
        public int CompanyId { get; set; }
        public Company Company { get; set; }
		
        //customer-Order relation (many to one)
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        //order-MenuItem relation (many to many)
        public ICollection<MenuItem> MenuItems { get; set; }
        public List<OrderMenuItem> OrderMenuItems { get; set; }
	}
}