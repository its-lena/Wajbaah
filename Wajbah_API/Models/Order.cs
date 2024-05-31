using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wajbah_API.Models
{
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
		public string Status { get; set; }
		public string Copoun { get; set; }
		public bool CashDelivered { get; set; }
		public string EstimatedTime { get; set; }
		//promocode-order relation (many to one)
		public int? PromoCodeId { get; set; } = null;
        public PromoCode? PromoCode { get; set; }
        //order-ExtraMenuItem relation (many to many)
        public ICollection<ExtraMenuItem> ExtraMenuItems { get; set; }
        public List<OrderExtraMenuItem> OrderExtraMenuItems { get; set; }

		//Company-Order relation (many to one)
		public int? CompanyId { get; set; } = null;
        public Company? Company { get; set; }
		
        //customer-Order relation (many to one)
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        //order-MenuItem relation (many to many)
        public List<MenuItem> MenuItems { get; set; }
        public List<OrderMenuItem> OrderMenuItems { get; set; }

        //Chef-Order relation (one to many)
        public string ChefId { get; set; } //NationalID
        public Chef Chef { get; set; }
    }
}
