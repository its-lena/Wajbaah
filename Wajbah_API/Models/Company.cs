using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wajbah_API.Models
{
	public class Company
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CompanyId { get; set; }
		[Required, MaxLength(50)]
		public string CompanyName { get; set; }
		public decimal Wallet { get; set; }
		[Required]
		public string Email { get; set; }
		[Required, MinLength(8)]
		public string Password { get; set; }
		[Required]
		public int PhoneNumber { get; set; }
		[Required]
		public decimal DeliveryFees { get; set; }
		[Required]
		public string contract { get; set; }
		[Required]
		public string Area { get; set; }
        //Company-Order relation (many to one)
        public List<Order> Orders { get; set; }
		
    }
}
