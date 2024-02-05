﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wajbah_API.Models
{
	public class OrderMenuItem
	{
		public int OrderId { get; set; }
		public Order Order { get; set; }
		public int MenuItemId { get; set; }
		public MenuItem MenuItem { get; set; }
		[Required]
		public int Quantity { get; set; }
		public string Size { get; set; }
		[NotMapped]
		public decimal TotalItem
		{
			get { return Quantity * MenuItem.SizePrices.FirstOrDefault(e => e.Size == Size).Price; }
		}
	}
}
