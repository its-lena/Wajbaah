using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wajbah_API.Models.DTO
{
	public class OrderCreateDTO
	{
		[Key]
		public int OrderId { get; set; }
		public string Notes { get; set; }
		[Required]
		public decimal TotalPrice { get; set; }
		[Required]
		public decimal SubTotal { get; set; }
		[Required]
		public decimal DeliveryFees { get; set; }
		[Required]
		public int DeliveryNumber { get; set; }
		[Required]
		public DateTime CreatedOn { get; set; }
		[Required]
		public DateTime DeliveryTime { get; set; }
		public List<int> Quanitities { get; set; }
		public List<string> Sizes { get; set; }
		public string Copoun { get; set; }
		[Required]
		public bool CashDelivered { get; set; }
		[Required]
		public string EstimatedTime { get; set; }
		public int? PromoCodeId { get; set; } = null;
		public int? CompanyId { get; set; } = null;
		[Required]
		public int CustomerId { get; set; }
        public List<int> MenuItemIds { get; set; }
		//test
		//public List<OrderMenuItem> orderMenuItems { get; set; }

		[Required]
		public string ChefId { get; set; }
    }
}
