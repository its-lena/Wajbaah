
namespace WajbahAdmin.Models.Dto
{
	public class OrderDto
	{
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public string ChefId { get; set; } = null!;
		public string? Status { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime Date { get; set; }
		public List<string> MenuItems { get; set; } = null!;
	}
}
