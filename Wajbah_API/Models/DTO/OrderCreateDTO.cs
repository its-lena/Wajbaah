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
		[Required]
		public string Status { get; set; }
		public string Copoun { get; set; }
		[Required]
		public bool CashDelivered { get; set; }
		[Required]
		public string EstimatedTime { get; set; }
		public int PromoCodeId { get; set; }
		public int CompanyId { get; set; }
		[Required]
		public int CustomerId { get; set; }
	}
}
