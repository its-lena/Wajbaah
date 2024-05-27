
namespace WajbahAdmin.Models.Dto
{
	public class OrderDTO
	{
		public int OrderId { get; set; }
		public decimal TotalPrice { get; set; }
		public decimal SubTotal {  get; set; }
		public decimal DeliveryFees { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime DeliveryTime { get; set; }
		//public string Status { get; set; }
		public string Copoun { get; set; }
		public bool CashDelivered { get; set; }
		public int PromoCodeId { get; set; }
		public int CustomerId { get; set; }
		public ICollection<Menu_ItemDTO> MenuItems { get; set; }
	}
}
