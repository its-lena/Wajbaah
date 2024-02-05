using System.ComponentModel.DataAnnotations.Schema;

namespace Wajbah_API.Models
{
	public class OrderExtraMenuItem
	{
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ExtraMenuItemId { get; set; }
        public ExtraMenuItem ExtraMenuItem { get; set; }
        public int Quantity { get; set; }
		[NotMapped]
		public decimal TotalItem
		{
			get { return Quantity * ExtraMenuItem.Price; }
		}
	}
}
