using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wajbah_API.Models
{
	public class Customer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CustomerId { get; set; }
		[Required]
		public int PhoneNumber { get; set; }
		public string Email { get; set; }
		[Required, MinLength(8)]
		public string Password { get; set; }
		[Required, MaxLength(25)]
		public string FirstName { get; set; }
		[Required, MaxLength(25)]
		public string LastName { get; set; }
		public string Role { get; set; } = "customer";
        [Required]
		public DateTime BirthDate { get; set; }
		public decimal Wallet { get; set; }
        public bool State { get; set; } = true;
        public string? Favourites { get; set; }
        public string? UsedCoupones { get; set; }
        //Customer-Order relation (many to one)
        public List<Order> Orders { get; set; }
    }
}
